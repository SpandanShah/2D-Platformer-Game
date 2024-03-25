using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private BoxCollider2D collider;
    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        Debug.Log("Player Controller awake");
    }
    // private void OnCollisionEnter2D(Collision2D collision){
    //     Debug.Log("Collision: "+ collision.gameObject.name);
    // }

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(speed));
        Vector3 scale = transform.localScale;

        if (speed < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (speed > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        bool isJumping = false;

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
        }
    }
}