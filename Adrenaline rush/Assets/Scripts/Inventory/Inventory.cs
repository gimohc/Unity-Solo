using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    [SerializeField] InputAction toggleInventory;
    bool inventoryIsOpen = false;
    Dictionary<InventoryItemData, int> inventory = new Dictionary<InventoryItemData, int>();

    void ToggleInventory()
    {
        if (toggleInventory.ReadValue<float>() > 0)
        {
            if (!inventoryIsOpen)
            {
                inventoryIsOpen = true;
                foreach (InventoryItemData item in inventory.Keys)
                {
                    // todo show inventory
                    Debug.Log(item.displayName + " x" + inventory[item]);
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
        InventoryItemData data = item.data;
        if (inventory.ContainsKey(data))
        {
            inventory[data] += amount;
        }
        else
            inventory.Add(data, amount);

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
