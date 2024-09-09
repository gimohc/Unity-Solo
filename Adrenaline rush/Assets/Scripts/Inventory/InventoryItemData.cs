using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Inventory Item Data")]
[Serializable]
public class InventoryItemData : ScriptableObject
{
    const int DEFAULTDAMAGE = 10;
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    public int entityDamage = DEFAULTDAMAGE;
    public int treeDamage = DEFAULTDAMAGE;
    public int mineDamage = DEFAULTDAMAGE;
    public float range = 40;

}
