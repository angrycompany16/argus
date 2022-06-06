using UnityEngine;

public class KeyItemPickup : MonoBehaviour
{
    [SerializeField] KeyItem keyItem;
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] Interactable interactable;

    void OnEnable() {
        interactable.OnInteract += GetPickup;
    }

    void OnDisable() {
        interactable.OnInteract -= GetPickup;
    }

    void GetPickup() {
        inventoryManager.AddKey(keyItem);
    }
}
