using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction move;
    [SerializeField] InputAction faceDirection;
    [SerializeField] InputAction jump;

    [SerializeField] float movementFactor = 20f;
    [SerializeField] float sensitivityFactor = 1f;
    [SerializeField] float jumpingFactor = 1f;
    //[SerializeField] float jumpHeight = 5f;
    [SerializeField] bool isJumping = false;
    private Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

    }
    void Start()
    {

        move.Enable();
        faceDirection.Enable();
        jump.Enable();

    }

    // Update is called once per frame

    void Update()
    {
        HandleDirection();
        HandleMovement();
        Jump();
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
        Vector2 directionInput = faceDirection.ReadValue<Vector2>() * Time.deltaTime * sensitivityFactor;
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
    void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.GetComponent<Floor>() != null) 
        isJumping = false;

    }


}
