using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory manager", menuName = "ScriptableObjects/InventoryManager", order = 1)]
public class InventoryManager : ScriptableObject
{
    public List<KeyItem> keys = new List<KeyItem>();

    public void AddKey(KeyItem key) {
        if (!keys.Contains(key))
            keys.Add(key);
    }
}
