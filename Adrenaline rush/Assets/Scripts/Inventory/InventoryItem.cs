using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem : MonoBehaviour
{

    // have serialized field drops as Inventory item for mining through constructor we initialize that   
    [SerializeField] public InventoryItemData data;
    [SerializeField] public int stackSize; 
    
    List<GameObject> players;

    private void Awake() { // limited amount of time to pick it up 
        StartCoroutine(DestroyObject());
        
    }
    public InventoryItem(){}
    public InventoryItem(InventoryItem item) {
        data = item.data;
        stackSize = item.stackSize;
        
    }
    public InventoryItem(InventoryItemData source, int amount)
    {
        data = source;
        stackSize = amount;
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject player in players)
        {
            if (other.gameObject == player)
            {
                Debug.Log("Picked up item");
                AddItemToPlayer(player);
            }

        }

    }
    public void SetStackSize(int stackSize)
    {
        this.stackSize = stackSize;
    }
    void AddItemToPlayer(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        inventory.AddItem(this);
    }
    private void Start()
    {
        // down side cant join mid game if done multiplayer.
        players = new List<GameObject>();
        GameObject playersContainer = FindObjectOfType<Players>().gameObject;
        foreach (Transform player in playersContainer.transform)
        {
            players.Add(player.gameObject);
        }
    }
    private IEnumerator DestroyObject() {
        yield return new WaitForSeconds(90f); 
        Destroy(gameObject);
    } 

}

