using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem : MonoBehaviour
{

    // have serialized field drops as Inventory item for mining through constructor we initialize that   
    [SerializeField] public InventoryItemData data;
    public int stackSize { get; private set; }
    List<GameObject> players;
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
    void AddItemToPlayer(GameObject player)
    {
        Inventory inventory = player.GetComponent<Inventory>();
        inventory.AddItem(this);

        Destroy(gameObject);

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

}

