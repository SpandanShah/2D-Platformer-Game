using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] public int TouchCount = 0;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TouchCount++;
        Debug.Log("Touch count: " + TouchCount);
        if (TouchCount == 1)
        {
            Debug.Log("Destroy Life 3");
            Destroy(life3);
        }
        else if (TouchCount == 2)
        {
            Destroy(life2);
        }
        else if (TouchCount == 3)
        {
            Destroy(life1);
        }
        if (collision.gameObject.GetComponent<PlayerController>() != null && TouchCount == 3)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.KillPlayer();
            TouchCount = 0;
        }
    }
}