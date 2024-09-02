using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
// using System.Diagnostics;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] public ScoreController scoreController;
    [SerializeField] public GameOverController gameOverController;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D coll;
    [SerializeField] private Rigidbody2D rb;
    public float speed;
    private bool isGrounded = true;
    [SerializeField] private float jumpForce = 10;
    
    private float screenBottom = -20f; // Define the bottom boundary of the screen
    //[SerializeField] private string Death = "Death";

    private void Awake()
    {
        Debug.Log("Player Controller awake");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the player GameObject!");
        }
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool VerticalInput = Input.GetKeyDown(KeyCode.Space);

        MoveCharactor(horizontal);
        PlayerMovementAnimation(horizontal, VerticalInput);

        if (transform.position.y < screenBottom)
        {
            Die();
        }
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
            SoundManager.Instance.Play(Sounds.PlayerMove);
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            SoundManager.Instance.Play(Sounds.PlayerMove);
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        //Jump
        if (VerticalInput && isGrounded)
        {
            SoundManager.Instance.Play(Sounds.PlayerJump);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    public void PickUpKey()
    {
        Debug.Log("Player picked up the key!");
        scoreController.IncreaseScore(10);
    }
    private void Die()
    {
        speed = 0;
        SoundManager.Instance.Play(Sounds.PlayerDeath);
        Scene Currentscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene("GameOver"); 
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
            Debug.Log("Player is grounded.");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "platform")
        {
            isGrounded = false;
        }
    }

    public void KillPlayer()
    {
        Debug.Log("Player killed by enemy!");
        
        if (gameObject.CompareTag("Player"))
        {
            animator.Play("Death",0,1.5f);
        }

        Die();
    }
}