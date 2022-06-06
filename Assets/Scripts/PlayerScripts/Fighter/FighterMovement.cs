using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] CustomAnimator anim;

    [Header("Stats")]
    [SerializeField] float moveSpeed;
    [SerializeField] float slideSpeed;
    [SerializeField] float slideDuration;

    [Header("Horizontal movement")]
    [Range(0, 1)]
    [SerializeField] float dampingBasic = 0.4f;
    [Range(0, 1)]
    [SerializeField] float dampingWhenStopping = 0.95f;
    [Range(0, 1)]
    [SerializeField] float dampingWhenTurning = 0.8f;

    float slideTime = 0f;

    Vector2 moveDir;

    bool isSliding = false;
    bool facingRight = true;

#region animations
    // const string RUN = "Player-run";
    // const string IDLE = "Idle";
#endregion

    void Update()
    {
        GetInput();
        if (!isSliding) {
            HandleAnimations();
            Movement();
        } else {
            slideTime += Time.deltaTime;
            if (slideTime >= slideDuration) StopSlide();
        }
    }

    // Gets character input...
    void GetInput() {
        moveDir = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (Input.GetKeyDown(KeyCode.Z)) {
            StartSlide();
        }

        if (Input.GetKeyUp(KeyCode.Z)) {
            StopSlide();
        }
    }

    void HandleAnimations() {

    }

    // Movement horizontal...
    void Movement() {
        if (moveDir.x > 0)
            facingRight = true;
        else if (moveDir.x < 0)
            facingRight = false;

        transform.localEulerAngles = facingRight ? new Vector3(0f, 0f, 0f) : new Vector3(0f, 180f, 0f);

    }

    void FixedUpdate() {
        if (!isSliding) {
            MoveCharacter();
        }
    }

    void StartSlide() {
        slideTime = 0f;
        isSliding = true;
        rb.velocity = moveDir * slideSpeed;
    }

    void StopSlide() {
        isSliding = false;
    }

    void MoveCharacter() {
        float hrzVelocity = rb.velocity.x;
        hrzVelocity += moveDir.x * moveSpeed;

        if (Mathf.Abs(moveDir.x) < 0.01f)
            hrzVelocity *= Mathf.Pow(1f - dampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(moveDir.x) != Mathf.Sign(hrzVelocity))
            hrzVelocity *= Mathf.Pow(1f - dampingWhenTurning, Time.deltaTime * 10f);
        else
            hrzVelocity *= Mathf.Pow(1f - dampingBasic, Time.deltaTime * 10f);
        
        rb.velocity = new Vector2(hrzVelocity, rb.velocity.y);
    }
}
