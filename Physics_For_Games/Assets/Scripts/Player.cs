using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    CharacterController cc = null;
    Animator animator = null;

    public float speed = 0.5f;
    public float pushPower = 3.0f;
    public float jumpVelocity = 10.0f;
    bool jumpInput = false;
    public bool isGrounded = true;

    Vector2 moveInput = new Vector2();
    public Vector3 velocity = new Vector3();

    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        jumpInput = Input.GetButton("Jump");

        //animator.SetFloat("Forwards", moveInput.y);
        //animator.SetBool("Jump", !isGrounded);

        animator.SetFloat("Speed", moveInput.y * speed * Time.deltaTime);

        Vector3 inputForce = new Vector3();
        inputForce.x = Input.GetAxis("Horizontal");
        inputForce.z = Input.GetAxis("Vertical");

        Vector3 forward = Vector3.Cross(Vector3.up, UnityEngine.Camera.main.transform.right) * inputForce.z; ;
        cc.Move(forward * -speed);

        Vector3 side = Vector3.Cross(Vector3.up, UnityEngine.Camera.main.transform.forward) * inputForce.x; ;
        cc.Move(side * speed);

        // player movement using WASD or arrow keys
        Vector3 delta = (moveInput.x * Vector3.right + moveInput.y * Vector3.forward) * speed;

        if (isGrounded || moveInput.x != 0 || moveInput.y != 0)
        {
            velocity.x = delta.x;
            velocity.z = delta.z;
        }

        // check for jumping
        if (jumpInput && isGrounded)
        {
            velocity.y = jumpVelocity;
        }

        // check if we've hit ground from falling. If so, remove our velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        // apply gravity after zeroing velocity so we register as grounded still
        velocity += Physics.gravity * Time.fixedDeltaTime;

        cc.Move(velocity * Time.deltaTime);
        isGrounded = cc.isGrounded;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDirection * pushPower;   
    }
}
