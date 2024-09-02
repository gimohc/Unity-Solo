using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerHit))]

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
    [SerializeField] bool isJumping = false;
    [SerializeField] float currentStamina;
    private Rigidbody rigidbody;
    private Player stats;
    private int maxStamina;
    private float movementFactor;
    [SerializeField] bool isRegenerating = false;

    void Awake()
    {
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
        float jumpInput = jump.ReadValue<float>();// * Time.deltaTime * jumpingFactor;
        if (jumpInput > 0 && !isJumping)
        {
            isJumping = true;
            rigidbody.AddForce(Vector3.up * jumpingFactor);

        }


    }
    public void Sprint()
    {
        
        if (sprint.ReadValue<float>() > 0 && currentStamina > 0)
        {
            StopCoroutine(StartStaminaRegen());
            isRegenerating = false;
            Debug.Log(Time.deltaTime);
            currentStamina = Mathf.Clamp(currentStamina - Time.deltaTime * 100, 0, maxStamina);
            
            movementFactor = defaultMovementFactor * 2;

        }
        else
        {
            if (currentStamina < 100 && !isRegenerating)
                StartCoroutine(StartStaminaRegen());
            movementFactor = defaultMovementFactor;
        }
    }

    private IEnumerator StartStaminaRegen()
    {
        isRegenerating = true;
        yield return new WaitForSeconds(0.7f);
        
        while(currentStamina < 100) {
            currentStamina = Mathf.Clamp(currentStamina + Time.deltaTime * 200, 0, maxStamina);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.GetComponent<Floor>() != null) 
        isJumping = false;

    }


}
