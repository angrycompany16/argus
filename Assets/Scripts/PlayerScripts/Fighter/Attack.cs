using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] float attackDuration;
    [SerializeField] BoxCollider2D attackHitbox; 

    void Update() {
        GetInput();
    }

    void GetInput() {
        if (Input.GetKeyDown(KeyCode.X)) {
            StartCoroutine(DoAttack());
        }
    }

    IEnumerator DoAttack() {
        attackHitbox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.enabled = false;
    }
}
