using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private AsyncOperation async;
    [SerializeField] GameObject optionsMenu;

    public void LoadNextScene(string name)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
