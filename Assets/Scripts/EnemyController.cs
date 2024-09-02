using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;
    [SerializeField] public LifeConroller lifeController;

    [SerializeField] private Animator enemyAnimator;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject groundDetector;
    [SerializeField] private float rayDistance;
    private int directionChanger = 1;

    void Update()
    {
        patrolEnemy();
    }

    private void patrolEnemy()
    {
        enemyAnimator.SetBool("Grounded", true);

        transform.Translate(directionChanger * Vector2.right * moveSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(groundDetector.transform.position, Vector2.down, rayDistance);

        if (!hit)
        {
            Vector3 scaleVector = transform.localScale;
            scaleVector.x *= -1;
            transform.localScale = scaleVector;
            directionChanger *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            lifeController.TakeDamage();
        }
    }

    public void PlayFootStep()
    {
        // Play the footstep sound or trigger any effect you want here
        Debug.Log("Footstep sound played!");
        // Example: SoundManager.Instance.Play(Sounds.Footstep); // if you have a sound manager
    }
}