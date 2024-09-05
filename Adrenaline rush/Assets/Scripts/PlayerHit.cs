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
    public static int defaultDamage = 10;
    public static float defaultRange = 40f;
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
        int damage = defaultDamage;
        float range = defaultRange;
        InventoryItem itemInHand = inventory.GetItemInHand();
        if (itemInHand != null)
        {
            range = itemInHand.data.range;
            damage = itemInHand.data.damage;
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out rayHit, range))
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
