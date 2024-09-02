using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeConroller : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;

    public GameObject[] lifeImages;

    public int MaxLives = 3;
    private int currentLives;
    
    void Start()
    {
        currentLives = MaxLives;
    }
    public void TakeDamage()
    {
        currentLives--;
        if (currentLives >= 0)
        {
            lifeImages[currentLives].SetActive(false);
        }
        if (currentLives <= 0)
        {
            playerController.KillPlayer();
        }
    }
}