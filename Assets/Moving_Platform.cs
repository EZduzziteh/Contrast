using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Moving_Platform : MonoBehaviour
{

    [SerializeField]
    bool isMovingUp = false;
    [SerializeField]
    float moveSpeed;

    Vector3 moveDirection;

    bool isReversed = false;

    Vector3 startPosition;
    [SerializeField]
    Vector3 endPosition;

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
        if (isReversed)
        {
            transform.position -= moveDirection * moveSpeed * Time.deltaTime;

            if (isMovingUp)
            {
                if(transform.position.y < startPosition.y)
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
