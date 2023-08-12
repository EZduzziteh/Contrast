
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

    private Camera mainCamera;
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "switch")
        {
            interactTextMesh.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "switch")
        {
            interactTextMesh.gameObject.SetActive(false);
        }
    }

    private void Initialize()
    {
        mainCamera = Camera.main;
        rb= GetComponent<Rigidbody2D>();
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
        
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        //update camera to follow player
        UpdateCameraPosition();
        
    }

    private void UpdateCameraPosition()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);

    }
    private void Jump()
    {
        
        rb.AddForce(transform.up * jumpForce);
    }
    private void HandleMovement()
    {
        transform.Translate(Input.GetAxis("Horizontal") * transform.right * moveSpeed * Time.deltaTime);
    }

  
}
