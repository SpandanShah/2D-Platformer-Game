using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] private BoxCollider2D collider;
    [SerializeField] private Rigidbody2D rb;
    public float speed;
    [SerializeField] private float jumpForce = 10;
    private bool isGrounded = false;
    private void Awake()
    {
        Debug.Log("Player Controller awake");
    }
    // private void OnCollisionEnter2D(Collision2D collision){
    //     Debug.Log("Collision: "+ collision.gameObject.name);
    // }

    private void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool VerticalInput = Input.GetKeyDown(KeyCode.Space);// Input.GetAxisRaw("Jump");
        float vertical = Input.GetAxisRaw("Vertical");
        MovePlayerVertically(vertical);


        MoveCharactor(horizontal);
        PlayerMovementAnimation(horizontal, VerticalInput);
        

        /*bool isJumping = false;

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            isJumping = true;
            animator.SetBool("Jump", true);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && isJumping && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            isJumping = false;
            animator.SetBool("Jump", false);
        }

        bool isCrouching = false;
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isCrouching)
        {
            isCrouching = true;
            animator.SetBool("Crouch", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isCrouching)
        {
            isCrouching = false;
            animator.SetBool("Crouch", false); // Transition to idle or walk state
        }*/
    }
    private void MoveCharactor(float horizontal)
    {
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
    }
    private void PlayerMovementAnimation(float horizontal, bool VerticalInput)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector3 scale = transform.localScale;

        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        //Jump
        if (VerticalInput)
        {
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            Debug.Log("Val of Vericle input: "+ VerticalInput);
        }
        //Debug.Log("Val v input:" + VerticalInput);
        /*
        else if(VerticalInput < 0)
        {
            Debug.Log("Val of Vericle input: " + VerticalInput);
            rb.AddForce(-Vector2.up * jumpForce,ForceMode2D.Impulse);
            Debug.Log("Collision: " + collider.gameObject.name);
        }
        */
    }
    public void MovePlayerVertically(float vertical)
    {
        if (vertical > 0 && isGrounded)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.transform.tag == "platform")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "platform")
        {
            isGrounded = false;
        }
    }
}