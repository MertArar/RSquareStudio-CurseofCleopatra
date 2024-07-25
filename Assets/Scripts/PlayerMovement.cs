using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 1.5f;
    [SerializeField] private GameObject player;

    private Animator animator;
    private Rigidbody rb;
     
    public static int currentTile = 0;
    
    private int next_x_pos;
    public float jumpForward = 1.5f;
    private float speedIncreaseInterval = 15f; 
    private float speedIncreaseAmount = 0.2f; 
    private float maxSpeed = 10f;
    private bool canMove = true;
    private bool canMoveLeftRight = true; // Sağ ve sol hareket kontrolü
    private bool Left, Right;
    private bool isDead = false;
    public float leftTrippingSpeed = 2f;
    public float rightTrippingSpeed = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDead) return; 

        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("Run", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Slide", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("Jump", true);
        }
        else if (Input.GetKeyUp(KeyCode.D) && canMove == true && canMoveLeftRight)
        {
            if (!animator.GetBool("Jump") && !animator.GetBool("Slide"))
                animator.SetBool("Right", true);
            else
                Right = true;

            if (rb.position.x >= -3 && rb.position.x < -1)
            {
                next_x_pos = 0;
            }
            else if (rb.position.x >= -1 && rb.position.x < 1)
            {
                next_x_pos = 2;
            }

            StartCoroutine(ToRight(next_x_pos));

        }
        else if (Input.GetKeyUp(KeyCode.A) && canMove == true && canMoveLeftRight)
        {
            if (!animator.GetBool("Jump") && !animator.GetBool("Slide"))
                animator.SetBool("Left", true);
            else
                Left = true;
            if (rb.position.x >= 1 && rb.position.x < 3)
            {
                next_x_pos = 0;
            }
            else if (rb.position.x >= -1 && rb.position.x < 1)
            {
                next_x_pos = -2;
                
            }

            
            StartCoroutine(ToLeft(next_x_pos));
            
        }
    }
    IEnumerator ToLeft(int next_x_pos)
    {
        canMove = false;

        float timer = 0.0125f;
        yield return new WaitForSeconds(timer);
        transform.position = new Vector3(this.next_x_pos + 0.8f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos + 0.6f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos + 0.4f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos + 0.2f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos, transform.position.y, transform.position.z + (playerSpeed/625)*100);

        canMove = true;
    }
    IEnumerator ToRight(int next_x_pos)
    {
        canMove = false;

        float timer = 0.0125f;
        yield return new WaitForSeconds(timer);
        transform.position = new Vector3(this.next_x_pos - 0.8f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos - 0.6f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos - 0.4f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos - 0.2f, transform.position.y, transform.position.z);
        yield return new WaitForSeconds(timer);

        transform.position = new Vector3(this.next_x_pos, transform.position.y, transform.position.z + (playerSpeed / 625)*100);

        canMove = true;
    }
    //Animation events
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
        if (isDead) return;

        if (animator.GetBool("FallDead"))
        {
            rb.MovePosition(rb.position + Vector3.down * animator.deltaPosition.magnitude);
        }

        if (animator.GetBool("LeftTripping"))
        {
            rb.MovePosition(rb.position + new Vector3(2f,0,0) * animator.deltaPosition.magnitude);
        }

        if (animator.GetBool("RightTripping"))
        {
            rb.MovePosition(rb.position + new Vector3(-0.35f,0,0) * animator.deltaPosition.magnitude);
        }
        
        else if (animator.GetBool("Jump"))
        {
            if (isJumpDown)
                rb.MovePosition(rb.position + new Vector3(0, 0, 1.5f) * animator.deltaPosition.magnitude);
            else
                rb.MovePosition(rb.position + new Vector3(0, 1.5f, 1.5f) * animator.deltaPosition.magnitude);
        }
        else if (animator.GetBool("Right"))
        {
            if (rb.position.x < next_x_pos)
                rb.MovePosition(rb.position + new Vector3(1, 0, 1.5f) * animator.deltaPosition.magnitude);
            else
                animator.SetBool("Right", false);
        }
        else if (animator.GetBool("Left"))
        {
            if (rb.position.x > next_x_pos)
                rb.MovePosition(rb.position + new Vector3(-1, 0, 1.5f) * animator.deltaPosition.magnitude);
            else
                animator.SetBool("Left", false);
        }
        
        else
        {
            float currentSpeed = Mathf.Min(playerSpeed, maxSpeed);
            rb.MovePosition(rb.position + Vector3.forward * animator.deltaPosition.magnitude * playerSpeed);
        }

        if (Left)
        {
            if (rb.position.x > next_x_pos)
                rb.MovePosition(rb.position + new Vector3(-1, 0, 0) * animator.deltaPosition.magnitude);
            else
                Left = false;
        }

        else if (Right)
        {
            if (rb.position.x < next_x_pos)
                rb.MovePosition(rb.position + new Vector3(1, 0, 0) * animator.deltaPosition.magnitude);
            else
                Right = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obs"))
        {
            animator.SetBool("Dead", true);
            collision.collider.enabled = false; 
            isDead = true;
            StartCoroutine(MoveBackwardOnDeath());
        }

        if (collision.collider.CompareTag("LeftTripping"))
        {
            animator.SetBool("LeftTripping", true);
            animator.SetBool("Dead", false);
            isDead = true;
            StartCoroutine(ResetTrippingState("LeftTripping"));
        }
        
        if (collision.collider.CompareTag("RightTripping"))
        {
            animator.SetBool("RightTripping", true);
            animator.SetBool("Dead", false);
            isDead = true;
            StartCoroutine(ResetTrippingState("RightTripping"));
        }

        if (collision.collider.CompareTag("HitTheLeg"))
        {
            animator.SetBool("HitTheLeg", true);
        }
    }

    [SerializeField] private GameObject cameraObj;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallDamage"))
        {
            cameraObj.transform.parent = null;
            animator.SetBool("FallDead", true);
        }
        else if (other.CompareTag("NoLeftRight"))
        {
            canMoveLeftRight = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NoLeftRight"))
        {
            canMoveLeftRight = true;
        }
    }

    private IEnumerator ResetTrippingState(string trippingState)
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetBool(trippingState, false);
        isDead = false;
    }

    private IEnumerator MoveBackwardOnDeath()
    {
        float moveTime = 0.5f;
        float elapsedTime = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2);

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }
}
