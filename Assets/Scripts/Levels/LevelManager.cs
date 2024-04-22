using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance {  get { return instance; } }
    public string[] Levels;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (GetLevelStatus(Levels[0]) == LevelStatus.Locked)
        {
            SetLevelStatus(Levels[0], LevelStatus.Unlocked);
        }
    }

    public void MarkCurrentLevelComplete()
    {
        Scene Currentscene = SceneManager.GetActiveScene();
        //set current level as complete
        SetLevelStatus(Currentscene.name, LevelStatus.Completed);
        //unlock next level
        /*int NextSceneIndex = Currentscene.buildIndex + 1;
        Scene NextScene = SceneManager.GetSceneByBuildIndex(NextSceneIndex);
        Debug.Log("Next Scene is valid: " + NextScene.IsValid());
        SetLevelStatus(NextScene.name, LevelStatus.Unlocked);*/
        int CurrentSceneIndex = Array.FindIndex(Levels, level => level == Currentscene.name);
        int NextSceneIndex = CurrentSceneIndex + 1;
        if(NextSceneIndex < Levels.Length)
        {
            SetLevelStatus(Levels[NextSceneIndex], LevelStatus.Unlocked);
        }
    }

    public LevelStatus GetLevelStatus(string level)
    {
        LevelStatus levelStatus = (LevelStatus) PlayerPrefs.GetInt(level, 0);
        return levelStatus;
    }

    public void SetLevelStatus(string level, LevelStatus levelStatus) 
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
        Debug.Log("Level: "+ level + ", Status: "+ levelStatus);
    }
}
