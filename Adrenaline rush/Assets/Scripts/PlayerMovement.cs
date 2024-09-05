using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerHit))]
[RequireComponent(typeof(Inventory))]

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction move;
    [SerializeField] InputAction faceDirection;
    [SerializeField] InputAction jump;
    [SerializeField] InputAction sprint;
    [SerializeField] float defaultMovementFactor = 20f;
    [SerializeField] float mouseSensitivityFactor = 200f;
    [SerializeField] float jumpingFactor = 1f;
    [SerializeField] bool isGrounded = false;
    float currentStamina; 
    bool isRegenerating = false;
    bool isSprinting = false;

    private Rigidbody rigidbody;
    private Player stats;
    private int maxStamina;
    private float movementFactor;
    Collider collider; // change when you change models


    void Awake()
    {
        collider = GetComponent<SphereCollider>();

        rigidbody = GetComponent<Rigidbody>();
        stats = GetComponent<Player>();

        maxStamina = stats.GetStamina();
        currentStamina = maxStamina;

        movementFactor = defaultMovementFactor;

    }
    void Start()
    {

        move.Enable();
        faceDirection.Enable();
        jump.Enable();
        sprint.Enable();

    }

    void Update()
    {
        HandleDirection();
        HandleMovement();
        Jump();
        Sprint();
    }
    // ws = y (front and back is the move.y) 
    // ad = x (left and right is the move.x)
    void HandleMovement()
    {
        Vector2 movementInput = move.ReadValue<Vector2>() * Time.deltaTime * movementFactor;
        float rotationY = transform.eulerAngles.y * Mathf.Deg2Rad;

        transform.Translate(Mathf.Cos(rotationY) * movementInput.x, 0, -Mathf.Sin(rotationY) * movementInput.x, Space.World);
        transform.Translate(Mathf.Sin(rotationY) * movementInput.y, 0, Mathf.Cos(rotationY) * movementInput.y, Space.World);


    }
    void HandleDirection()
    {
        Vector2 directionInput = faceDirection.ReadValue<Vector2>() * Time.deltaTime * mouseSensitivityFactor;
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x - directionInput.y,
            transform.localEulerAngles.y + directionInput.x,
            0
            );
    }
    private void Jump()
    {
        float jumpInput = jump.ReadValue<float>();
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, collider.bounds.extents.y + 0.1f);
        if (jumpInput > 0 && isGrounded)
        {
            rigidbody.AddForce(Vector3.up * jumpingFactor, ForceMode.Impulse);
            
        }
        if(rigidbody.velocity.y < float.Epsilon && !isGrounded) 
            rigidbody.AddForce(Physics.gravity * 2.5f, ForceMode.Acceleration);


    }
    public void Sprint()
    {

        if (sprint.ReadValue<float>() > 0 && !isSprinting)
        {
            StartCoroutine(DecreaseStamina());
            movementFactor = 2 * defaultMovementFactor;

        }
        else if (sprint.ReadValue<float>() <= 0)
        {
            if (currentStamina < 100 && !isRegenerating)
            {
                StartCoroutine(StartStaminaRegen());
            }
            movementFactor = defaultMovementFactor;
        }
        
    }
    private IEnumerator DecreaseStamina()
    {
        isRegenerating = false;
        isSprinting = true;
        
        while (currentStamina > 0 && isSprinting)
        {
            currentStamina = Mathf.Clamp(currentStamina -2, 0, maxStamina);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator StartStaminaRegen()
    {
        isSprinting = false;
        isRegenerating = true;
        yield return new WaitForSeconds(0.7f);

        while (currentStamina < 100 && isRegenerating)
        {
            currentStamina = Mathf.Clamp(currentStamina + 3, 0, maxStamina);
            yield return new WaitForSeconds(0.2f);
        }
    }


}
