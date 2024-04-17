using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbController : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Scene 1";
    public void PlayButton()
    {
        SceneManager.LoadScene(newGameLevel);
    }
}
