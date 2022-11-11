using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;

    public float jumForce;

    [Header("Ground Check")]
    public Transform grondCheck;
    public float checkRadius;
    public LayerMask grondLayer;

    [Header("States check")]
    public bool isGround;
    public bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
        Movement();
        Jump();
    }

    void CheckInput()
    {
        if(Input.GetButtonDown("Jump") && isGround)
        {
            canJump = true;
        }
    }

    void Movement()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");

        float horizontalInput = Input.GetAxisRaw("Horizontal");


        rb.velocity = new Vector2(horizontalInput*speed, rb.velocity.y);

        if(horizontalInput != 0)
        {
            transform.localScale = new Vector3(horizontalInput, 1, 1);
        }
    }

    void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumForce);
            rb.gravityScale = 4;
            canJump = false;
        }
    }

    void PhysicsCheck()
    {
        isGround = Physics2D.OverlapCircle(grondCheck.position, checkRadius, grondLayer);

        if (isGround)
        {
            rb.gravityScale = 1;
        } else
        {
            rb.gravityScale = 4;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(grondCheck.position, checkRadius);
    }
}
