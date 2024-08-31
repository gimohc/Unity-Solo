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

    /*
    private int strength = 1;
    private int hp = 10;
    private float jumpHeight = 1;
    private int bonusJump = 0;
    private float movementSpeed = 1;
    private int stamina = 1;
    */


    private int layerMask = 1 << 8;
    [SerializeField] InputAction openChest;
    void Start()
    {
        openChest.Enable();
    }
    void Update()
    {
        if (openChest.ReadValue<float>() > 0)
        { 
            findChest();
        }
    }
    void findChest()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 6f, layerMask))
        {
            Chest chest = hit.collider.gameObject.GetComponent<Chest>();
            chest.Open();
        }
    }
}
