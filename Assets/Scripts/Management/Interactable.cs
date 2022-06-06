using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interactable : MonoBehaviour
{
    bool canInteract = false;
    [SerializeField] GameObject canInteractText;

    public delegate void Interaction();
    public event Interaction OnInteract;

    void OnTriggerEnter2D(Collider2D other) {
        // Show Text
        if (other.CompareTag("Player")) {
            canInteract = true;
            canInteractText.SetActive(true);
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        // Remove Text
        if (other.CompareTag("Player")) {
            canInteract = false;
            canInteractText.SetActive(false);
        }
    }

    void Update() {
        if (canInteract) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                OnInteract();
            }
        }
    }
}
