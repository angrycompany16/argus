// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;

    [Header("Coyote time + jump buffer")]
    float timeSinceJumpPressed;
    [SerializeField] float jumpBufferDuration;
    float timeSinceLeftGrounded;
    [SerializeField] float jumpRememberTime;

    [Header("Stats")]
    [SerializeField] float moveSpeed;
    [SerializeField] float cutJumpSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float customGravityScale;
    [SerializeField] float extraFallMultiplier;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float ledgeFallSpeed;

    [Header("Horizontal movement")]
    [Range(0, 1)]
    [SerializeField] float dampingBasic = 0.4f;
    [Range(0, 1)]
    [SerializeField] float dampingWhenStopping = 0.95f;
    [Range(0, 1)]
    [SerializeField] float dampingWhenTurning = 0.8f;
    [Range(0, 1)]
    [SerializeField] float airDampingBasic = 0.4f;
    [Range(0, 1)]
    [SerializeField] float airDampingWhenStopping = 0.4f;
    [Range(0, 1)]
    [SerializeField] float airDampingWhenTurning = 0.5f;

    [Space]
    [SerializeField] float fallSmoothness;
    [SerializeField] float fallSmoothnessDropping;

    [Space]
    [SerializeField] Vector2 boxCastSize;
    [SerializeField] float groundCheckDist;

    Vector2 moveDir;

    bool isGrounded;
    bool wasGrounded;
    bool jumpBuffered;
    bool jumpRemembered;
    bool isJumping;

    void Start()
    {
        rb.gravityScale = customGravityScale;
    }

    void Update()
    {
        JumpCheck();
        GetInput();
        Movement();

        wasGrounded = isGrounded;
    }

    // Gets character input...
    void GetInput() {
        moveDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (Input.GetKeyDown(KeyCode.Z)) {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Z)) {
            CutJump();
        }
    }

    // Movement horizontal...
    void Movement() {
        if (!isGrounded) {
            // Coyote time
            timeSinceLeftGrounded += Time.deltaTime;
            if (timeSinceLeftGrounded < jumpRememberTime) 
                jumpRemembered = true;
            else 
                jumpRemembered = false;

            // Jump buffer
            timeSinceJumpPressed += Time.deltaTime;
            if (timeSinceJumpPressed < jumpBufferDuration) {
                jumpBuffered = true;
            }
            else {
                jumpBuffered = false;
            }

            if (rb.velocity.y < 5f)
                rb.velocity -= new Vector2(0f, extraFallMultiplier * Time.deltaTime);
        }

        // Upon landing...
        if (!wasGrounded && isGrounded) {
            if (jumpBuffered)
                Jump();
        }

        // Upon jumping...
        if (wasGrounded && !isGrounded) {
            timeSinceLeftGrounded = 0f;
        }

        if (rb.velocity.y < -maxFallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
    }

    void FixedUpdate() {
        MoveCharacter();
    }

    void MoveCharacter() {
        float hrzVelocity = rb.velocity.x;
        hrzVelocity += moveDir.x * moveSpeed;

        if(isGrounded) {
            if (Mathf.Abs(moveDir.x) < 0.01f)
                hrzVelocity *= Mathf.Pow(1f - dampingWhenStopping, Time.deltaTime * 10f);
            else if (Mathf.Sign(moveDir.x) != Mathf.Sign(hrzVelocity))
                hrzVelocity *= Mathf.Pow(1f - dampingWhenTurning, Time.deltaTime * 10f);
            else
                hrzVelocity *= Mathf.Pow(1f - dampingBasic, Time.deltaTime * 10f);
        } else {
            if (Mathf.Abs(moveDir.x) < 0.01f)
                hrzVelocity *= Mathf.Pow(1f - airDampingWhenStopping, Time.deltaTime * 10f);
            else if (Mathf.Sign(moveDir.x) != Mathf.Sign(hrzVelocity))
                hrzVelocity *= Mathf.Pow(1f - airDampingWhenTurning, Time.deltaTime * 10f);
            else
                hrzVelocity *= Mathf.Pow(1f - airDampingBasic, Time.deltaTime * 10f);
        }
        
        rb.velocity = new Vector2(hrzVelocity, rb.velocity.y);
    }

    void JumpCheck() {
        isGrounded = Physics2D.BoxCast(transform.position, boxCastSize, 1f, Vector2.down, groundCheckDist, groundLayer);
    }

    void Jump() {
        timeSinceJumpPressed = 0f;

        if (isGrounded || jumpRemembered) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    void CutJump() {
        if (rb.velocity.y > -2f) {
            rb.velocity = new Vector3(rb.velocity.x, -cutJumpSpeed);
        }
    }
}
