using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigidbody;

    public int jumpForce = 3;
    [SerializeField] public float playerSpeed = 2.5f;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Run", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Slide", true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Jump", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Right", true);
        }
    }

    //Animation Events
    void ToggleOff(string Name)
    {
        animator.SetBool(Name, false);
        isJumpDown = false;
    }

    private bool isJumpDown = false;
    
    void JumpDown()
    {
        isJumpDown = true;
    }
    
    private void OnAnimatorMove()
    {
        if (animator.GetBool("Jump"))
        {
            if (isJumpDown)
            {
                rigidbody.MovePosition(rigidbody.position + new Vector3(0,0,0f) * animator.deltaPosition.magnitude);
            }
            else
            {
                rigidbody.MovePosition(rigidbody.position + new Vector3(0,1.5f,0f) * animator.deltaPosition.magnitude);
            }
            
        }
        else
           rigidbody.MovePosition(rigidbody.position + Vector3.forward * animator.deltaPosition.magnitude * playerSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obs")
        {
            
        }
    }
}
