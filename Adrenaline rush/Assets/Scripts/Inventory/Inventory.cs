using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{

    [SerializeField] InputAction toggleInventory;
    [SerializeField] InputAction scrollThroughItems;
    bool inventoryIsOpen = false;
    int heldIndex = 0; // goes 0-9 returns the index the player has in hand right now 
    [SerializeField] InventoryItem[] inventory = new InventoryItem[10];

    private void Awake()
    {

    }
    void ToggleInventory()
    {
        if (toggleInventory.ReadValue<float>() > 0)
        {
            if (!inventoryIsOpen)
            {
                inventoryIsOpen = true;
                for (int i = 0; i < inventory.Count(); i++)
                {
                    // todo show inventory
                    InventoryItem item = inventory[i];
                    if (item == null) continue;
                    Debug.Log(item.data.displayName + " x" + item.stackSize);
                }

            }

            inventoryIsOpen = !inventoryIsOpen;
        }
    }
    public void AddItem(InventoryItem item)
    {

        for (int i = 0; i < inventory.Count(); i++)
        {
            InventoryItem current = inventory[i];
            if (current == null)
                continue;

            if (current.data == item.data)
            {
                current.stackSize += item.stackSize;
                Destroy(item.gameObject);
                return;
            }
        }
        int availableIndex = FindFirstAvailableIndex();
        if (availableIndex != -1)
        {
            GameObject gameObject = new GameObject("placeholder for items");
            InventoryItem newItem = gameObject.AddComponent<InventoryItem>();// new InventoryItem(item.data, item.stackSize);
            newItem.data = item.data;
            newItem.stackSize= item.stackSize;
            

            inventory[availableIndex] = newItem;

            Destroy(item.gameObject);
        }
    }
    public int FindFirstAvailableIndex()
    {
        for (int i = 0; i < inventory.Count(); i++)
        {
            if (inventory[i] == null)
                return i;
        }
        return -1;
    }
    public InventoryItem GetItemAtIndex(int index)
    {
        return inventory[index];
    }
    public InventoryItem GetItemInHand()
    {
        inventory.ElementAt(heldIndex);
        return inventory[heldIndex];
    }
    private void Scroll()
    {
        float scroll = scrollThroughItems.ReadValue<float>();
        if (scroll == 0) return;
        else if (scroll > 0)
            heldIndex = (heldIndex + 1) % 10;
        else
            heldIndex = (heldIndex + 9) % 10;

    }

    void Start()
    {
        toggleInventory.Enable();
        scrollThroughItems.Enable();
    }

    void Update()
    {
        ToggleInventory();
        Scroll();

    }
}
