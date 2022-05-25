using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;

    // hei hei

    [Header("Stats")]    
    [SerializeField] float moveSpeed;
    [SerializeField] float cutJumpSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float fallForce;
    [SerializeField] float customGravityScale;
    [SerializeField] float maxFallSpeed;

    [Space]
    [SerializeField] float groundedBaseSmoothness;
    [SerializeField] float groundedMoveSmoothness;
    [SerializeField] float groundedStopSmoothness;

    [Space]
    [SerializeField] float airBaseSmoothness;
    [SerializeField] float airMoveSmoothness;
    [SerializeField] float airStopSmoothness;

    [Space]
    [SerializeField] float fallSmoothness;
    [SerializeField] float fallSmoothnessDropping;

    [Space]
    [SerializeField] Vector2 boxCastSize;
    [SerializeField] float groundCheckDist;

    Vector2 moveDir;
    Vector2 desiredVelocity;

    bool isGrounded;
    bool wantsToJump;
    bool canJump;
    bool isJumping;

    void Start()
    {
        rb.gravityScale = customGravityScale;
    }

    void Update()
    {
        GetInput();
        MovePlayer();
        LerpMove();
        JumpCheck();
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
    void MovePlayer() {
        desiredVelocity = new Vector2(
            moveDir.x * moveSpeed,
            desiredVelocity.y
        );

        if (!isGrounded)
            desiredVelocity.y -= 9.18f * customGravityScale * Time.deltaTime;
    }

    void LerpMove() {
        float xVel = 0f;
        float yVel = 0f;
        if (isGrounded) {
            if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(moveDir.x)) {
                xVel = Mathf.Lerp(rb.velocity.x, desiredVelocity.x, Time.deltaTime * groundedMoveSmoothness);
            } else if (moveDir.x == 0) {
                xVel = Mathf.Lerp(rb.velocity.x, desiredVelocity.x, Time.deltaTime * groundedStopSmoothness);
            } else {
                xVel = Mathf.Lerp(rb.velocity.x, desiredVelocity.x, Time.deltaTime * groundedBaseSmoothness);
            }

            yVel = Mathf.Lerp(rb.velocity.y, desiredVelocity.y, Time.deltaTime * fallSmoothness);
        } else {
            if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(moveDir.x)) {
                xVel = Mathf.Lerp(rb.velocity.x, desiredVelocity.x, Time.deltaTime * airMoveSmoothness);
            } else if (moveDir.x == 0) {
                xVel = Mathf.Lerp(rb.velocity.x, desiredVelocity.x, Time.deltaTime * airStopSmoothness);
            } else {
                xVel = Mathf.Lerp(rb.velocity.x, desiredVelocity.x, Time.deltaTime * airBaseSmoothness);
            }

            if (Mathf.Sign(rb.velocity.y) != Mathf.Sign(desiredVelocity.y)) {
                yVel = Mathf.Lerp(rb.velocity.y, desiredVelocity.y, Time.deltaTime * fallSmoothnessDropping);
            } else {
                yVel = Mathf.Lerp(rb.velocity.y, desiredVelocity.y, Time.deltaTime * fallSmoothness);
            }
        } 

        rb.velocity = new Vector2(xVel, yVel);
    }

    void JumpCheck() {
        isGrounded = Physics2D.BoxCast(transform.position, boxCastSize, 1f, Vector2.down, groundCheckDist, groundLayer);

        if(rb.velocity.y < -maxFallSpeed)
            desiredVelocity = new Vector2(desiredVelocity.x, -maxFallSpeed);
    }

    void Jump() {
        if (isGrounded) {
            desiredVelocity = new Vector3(desiredVelocity.x, jumpSpeed);
        }
    }

    void CutJump() {
        if (rb.velocity.y != 0) {
            desiredVelocity = new Vector2(desiredVelocity.x, cutJumpSpeed);
        }
    }
}
