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
    [SerializeField] GameObject nextLevel;


    public CharacterData characterData;
    public UserIdCheck loggedUser;


    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            Localization.Localizer();
        }
        //DontDestroyOnLoad(this);

    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            optionsMenu.SetActive(!optionsMenu.activeSelf);
        }
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
        }
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

    public void NextLevel()
    {
        Time.timeScale = 0;
        Character.Instance.lvl = 1;
        if (loggedUser.objectId == null)
        {
            Persistance.SaveData(Character.Instance.getCharacterData(), "Local");
        }
        else
        {
            Persistance.SaveData(Character.Instance.getCharacterData(), loggedUser.objectId);
        }
        StartCoroutine(PlayerLogin.UpdatePlayerBackUp(Character.Instance.getCharacterData()));
        nextLevel.SetActive(true);
    }
}
