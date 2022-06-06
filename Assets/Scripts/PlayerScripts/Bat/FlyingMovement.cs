using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float flySpeed;

    float oldGravScale;

    Vector2 moveDir;

    void OnEnable() {
        oldGravScale = rb.gravityScale;
        rb.gravityScale = 0f;
    }

    void OnDisable() {
        rb.gravityScale = 0f;
        rb.gravityScale = oldGravScale;
    }

    void Start() {
    }

    void Update() {
        GetInput();
        Move();
    }

    void GetInput() {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
    }

    void Move() {
        rb.velocity = moveDir.normalized * flySpeed;
    }
}
