using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    [SerializeField] Rigidbody2D rb;

    [SerializeField] PlayerMovement moveScript;


    void Update() {
        GetInput();        
    }

    void GetInput() {
        if (Input.GetKeyDown(KeyCode.C)) {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            StartCoroutine(DashCoroutine(input));
        }
    }

    IEnumerator DashCoroutine(Vector2 input) {
        moveScript.isDashing = true;
        
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(input * dashSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);

        moveScript.isDashing = false;
    }
}
