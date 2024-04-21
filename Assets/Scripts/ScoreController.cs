using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoretext;
    private int score = 0;
    private void Awake()
    {
        //TextMeshProUGUI textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        //scoretext = textMeshProUGUI;
    }

    private void Start()
    {
        RefreshUI();
    }

    public void IncreaseScore(int increment)
    {
        Debug.Log("Inside Score Controller");
        score += increment;
        RefreshUI();
    }

    private void RefreshUI()
    {
        scoretext.text = "Score: " + score;
    }
}