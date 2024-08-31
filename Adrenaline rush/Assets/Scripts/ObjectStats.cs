using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStats : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int hp = 100;
    InventoryItem drops;
    [SerializeField] InventoryItemData inventoryItemData;
    void Start()
    {
        
        drops = new InventoryItem(inventoryItemData, UnityEngine.Random.Range(1,4)); // 1 inclusive 4 exclusive
    }
    public void Damage(int damage) {
        hp-=damage;
        if(hp <= 0) {
            // todo drop items
        }

    }


}
