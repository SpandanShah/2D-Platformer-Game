using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbController : MonoBehaviour
{
    [SerializeField] private string LevelSelection = "LevelSelection";
    public void PlayButton()
    {
        SceneManager.LoadScene(LevelSelection);
    }
    public void QuitButton()
    {
        Debug.Log("Inside Quit");
        Application.Quit();
    }
}
