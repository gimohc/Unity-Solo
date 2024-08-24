using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    // attach weapon scripts to all weapons, all inherit from a main weapon script

    private int strength = 1;
    private int hp = 10;
    private float jumpHeight = 1;
    private int bonusJump = 0;
    private float movementSpeed = 1;
    private int stamina = 1;
    private int layerMask = 1 << 8;
    [SerializeField] InputAction openChest;
    void Start()
    {
        openChest.Enable();
        //layerMask = ~layerMask;

    }

    // Update is called once per frame
    void Update()
    {
        if (openChest.ReadValue<float>() > 0)
        { //&& findChest()) {
            Debug.Log("e pressed");

            findChest();
        }
    }
    void findChest()
    {
        //   RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 6f, layerMask))
        {
            Debug.Log("found chest");
        }
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    }
}
