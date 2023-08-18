
using System;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using TMPro;
public class PlayerControl : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float jumpForce = 1.0f;
    [SerializeField]
    private GameObject interactTextMesh;
    [SerializeField]
    private LayerMask platformLayerMask;
    [SerializeField]
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField]
    private float jumpBufferTime = 0.5f;
    private float jumpBufferCounter;

    private Camera mainCamera;
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    Vector3 lastCheckPoint;

    bool isOnLadder = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "switch")
        {
            interactTextMesh.SetActive(true);
        }

        if(collision.tag == "ladder")
        {
            isOnLadder = true;
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0.0f;
        }

        if (collision.tag == "deathBox")
        {
            transform.position = lastCheckPoint;
        }

        if (collision.tag == "checkPoint")
        {
            lastCheckPoint = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "switch")
        {
            interactTextMesh.gameObject.SetActive(false);
        }
        if (collision.tag == "ladder")
        {
            isOnLadder = false;

            rb.gravityScale = 1.0f;
        }
    }

    private void Initialize()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        lastCheckPoint = transform.position;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        //return if input is not from the controlled players client
        if (!Application.isFocused) return;

        //otherwise, handle player inputs
        HandleMovement();

        if (IsGrounded())
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        {
            Jump();
        }
        //if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
        //{
        //    Debug.Log(jumpBufferCounter);
        //    rb.velocity = new Vector2(rb.velocity.x, jumpForce*0.05f);
        //    jumpBufferCounter = 0f;
        //}
        //
        //if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        //    coyoteTimeCounter = 0f;
        //}

    
        
    }

   
    private void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.up * jumpForce);
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
    }
    private void HandleMovement()
    {
        transform.Translate(Input.GetAxis("Horizontal") * transform.right * moveSpeed * Time.deltaTime);
        if (isOnLadder)
        {
            transform.Translate(Input.GetAxis("Vertical") * transform.up * moveSpeed * Time.deltaTime);
        }
    }
    private bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D rayHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size - new Vector3(0.2f,0f,0f), 0f, Vector2.down, extraHeight, platformLayerMask);
        //Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.e)
        return rayHit.collider != null;
    }
  
}
