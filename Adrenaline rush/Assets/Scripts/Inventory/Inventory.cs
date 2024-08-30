using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    // store all items in list/arraylist 
    // for each item show it 
    [SerializeField] InputAction toggleInventory;
    bool inventoryIsOpen = false;
    List<InventoryItem> inventory = new List<InventoryItem>();

    void ToggleInventory() {
        if(toggleInventory.ReadValue<float>() > 0) {
            if(!inventoryIsOpen)
                foreach  (InventoryItem item in inventory) {
                    // todo show inventory 
                }
            else {
                //todo hide inventory
            }
            inventoryIsOpen = !inventoryIsOpen;
        }
    }
    public void AddItem(InventoryItem item) {
        inventory.Add(item);

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
