using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FormUpgrade : MonoBehaviour
{
    [SerializeField] int ID;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            Shapeshift shapeshiftScript = other.gameObject.GetComponentInParent<Shapeshift>();

            shapeshiftScript.enabledForms.Add(shapeshiftScript.forms[ID]);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
