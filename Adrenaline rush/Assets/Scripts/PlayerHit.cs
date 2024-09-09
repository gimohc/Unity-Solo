using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Inventory))]
public class PlayerHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] InputAction swing;
    bool isSwinging = false;
    Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
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
        float range = 0;

        InventoryItem itemInHand = inventory.GetItemInHand();
        if (itemInHand != null)
        {
            range = itemInHand.data.range;
            //int damage = itemInHand.data.damage;
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out rayHit, range))
        {
            GameObject objectHit = rayHit.collider.gameObject;
            if (objectHit != null)
            {
                ObjectStats stats = objectHit.GetComponent<ObjectStats>();
                if (stats != null)
                {
                    //stats.DealDamage(damage);
                }

            }
        }
        yield return new WaitForSeconds(1f); //placeholder replace with weapon speed
        isSwinging = false;
    }
}
