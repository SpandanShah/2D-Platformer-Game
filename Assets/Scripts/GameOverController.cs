using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private string GameLevel = "Scene 1";
    [SerializeField] private string Lobby = "Lobby";
    public void ResartButton()
    {
        SceneManager.LoadScene(GameLevel);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(Lobby);
    }
}
