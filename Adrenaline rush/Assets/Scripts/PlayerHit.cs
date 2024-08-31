using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction swing; 
    [SerializeField] float range = 5f;
    bool isSwinging = false;
    int layer = 9 << 1;

    void Start()
    {
        swing.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if(swing.ReadValue<float>() > 0 && ! isSwinging) {
            StartCoroutine(hitCoroutine());
        }
    }
    private IEnumerator hitCoroutine() {
        isSwinging = true;
        RaycastHit rayHit;
        Physics.Raycast(transform.position, Vector3.forward, out rayHit, range); // could differentiatea range from weapon to others

        GameObject objectHit = rayHit.collider.gameObject;
        if(objectHit != null && objectHit.GetComponent<ObjectStats>()) {

        }

        yield return new WaitForSeconds(1f); //placeholder replace with weapon speed
        isSwinging = false;
    }
}
