using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [SerializeField] DoorProperties properties;
    [SerializeField] BoxCollider2D col;
    [SerializeField] Interactable interactable;
    [SerializeField] InventoryManager inventory;
    bool opened;

    void Awake() {
        if (properties.open || properties.otherSide.open) {
            SetOpened();
        }
    }

    void OnEnable() {
        interactable.OnInteract += TryOpen;
    }

    void OnDisable() {
        interactable.OnInteract -= TryOpen;
    }

    void TryOpen() {
        if (opened) return;
        foreach (KeyItem key in inventory.keys) {
            if (key.opens == properties) {
                col.enabled = false;
                opened = true;
            }
        }
    }

    void SetOpened() {
        col.enabled = false;
    }
}
