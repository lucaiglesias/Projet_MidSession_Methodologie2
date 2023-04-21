using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject gameOverMenu;

    public BackUP userData;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        Localization.Localizer();

    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }
}
