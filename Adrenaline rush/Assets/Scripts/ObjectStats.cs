using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int hp = 100;
    [SerializeField] InventoryItemData inventoryItemData;
    Pouch pouches;
    private GameObject pouch; // pouch prefab which is going to have drops inside 
    InventoryItem drops;// 

    void Start()
    {
        pouches = FindObjectOfType<Pouch>();
        pouch = pouches.GetPouch();
        drops = pouch.GetComponent<InventoryItem>();
    }
    public void DealDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // todo drop items spawn within a circle the drops could get random
            // for now its gonna be spawning in the exact same spot it was destroyed
            drops.data = inventoryItemData;
            drops.SetStackSize(Random.Range(1, 4));
            Instantiate(pouch, transform.position + Vector3.up * 5, Quaternion.identity, pouches.gameObject.transform);
            Destroy(gameObject);
        }

    }


}