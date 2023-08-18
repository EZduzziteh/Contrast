using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
using System;

public class Moving_Platform : NetworkBehaviour
{
    [Tooltip("True if platform should be moving.")]
    public NetworkVariable<bool> isFrozen = new NetworkVariable<bool>();
    
    
    [SerializeField]
    [Tooltip("Whether the platform moves horizontally or vertically.")]
    bool isMovingUp = false;
    [SerializeField]
    [Tooltip("How fast the platform moves")]
    float moveSpeed;

    Vector3 moveDirection;

    bool isReversed = false;

    Vector3 startPosition;

    [SerializeField]

    [Tooltip("The farthest position that the platform can go to before reversing. Note: if platform moves horizontally, only the 'x' value of this transform is used.")]
    Vector3 endPosition;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        isFrozen.OnValueChanged += OnValueChanged;
    }

    private void OnValueChanged(bool previousValue, bool newValue)
    {
        isFrozen.Value = newValue;
    }

    private void Start()
    {

        startPosition = transform.position;
        
        if (isMovingUp)
        {
            moveDirection = transform.up;
        }
        else
        {
            moveDirection = transform.right;
        }
        

    }



    private void Update()
    {
        if (!isFrozen.Value)
        {
            if (isReversed)
            {
                transform.position -= moveDirection * moveSpeed * Time.deltaTime;

                if (isMovingUp)
                {
                    if (transform.position.y < startPosition.y)
                    {
                        isReversed = false;

                    }
                }
                else
                {
                    if (transform.position.x < startPosition.x)
                    {
                        isReversed = false;
                    }
                }
            }
            else
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;

                if (isMovingUp)
                {
                    if (transform.position.y > endPosition.y)
                    {
                        isReversed = true;
                    }
                }
                else
                {
                    if (transform.position.x > endPosition.x)
                    {
                        isReversed = true;
                    }
                }

            }
        }
    }



}
