using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
 
    [SerializeField] InputAction toggleInventory;
    bool inventoryIsOpen = false;
    Dictionary<InventoryItem, int> inventory = new Dictionary<InventoryItem, int>();

    void ToggleInventory()
    {
        if (toggleInventory.ReadValue<float>() > 0)
        {
            if (!inventoryIsOpen) {
                inventoryIsOpen = true;
                foreach (InventoryItem item in inventory.Keys)
                {
                    // todo show inventory
                    Debug.Log(item.data.displayName + " x" + item.stackSize);
                }

            }
            else
            {
                inventoryIsOpen = false;
                //todo hide inventory
            }
            inventoryIsOpen = !inventoryIsOpen;
        }
    }
    public void AddItem(InventoryItem item)
    {
        int amount = item.stackSize;
        if (inventory.ContainsKey(item))
            inventory[item] += amount;
        else
            inventory.Add(item, amount);

    }


    // Start is called before the first frame update
    void Start()
    {
        toggleInventory.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ToggleInventory();

    }
}
