using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public static StartButton Instance { get; private set; }


    //based on class exercice

    [SerializeField] SceneData startGame_Scene;
    [SerializeField] SceneData quitGame_Scene;
    public void OnStartClick()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene(startGame_Scene.sceneName);


    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene(quitGame_Scene.sceneName);
    }


}
