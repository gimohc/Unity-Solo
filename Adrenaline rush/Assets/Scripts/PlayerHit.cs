using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction swing;
    [SerializeField] float range = 80f;
    bool isSwinging = false;
    int damage = 20; // placeholder change it according to the weapon held

    void Start()
    {
        swing.Enable();
    }

    void Update()
    {
        if (swing.ReadValue<float>() > 0 && !isSwinging)
        {
            StartCoroutine(hitCoroutine());
        }
    }
    private IEnumerator hitCoroutine()
    {
        isSwinging = true;
        RaycastHit rayHit;
        if(Physics.Raycast(transform.position, Vector3.forward, out rayHit, range)) 
        {
            GameObject objectHit = rayHit.collider.gameObject;
            if (objectHit != null)
            {
                ObjectStats stats = objectHit.GetComponent<ObjectStats>();
                if (stats != null)
                {
                    stats.DealDamage(damage);
                }

            }
        }
        yield return new WaitForSeconds(1f); //placeholder replace with weapon speed
        isSwinging = false;
    }
}
