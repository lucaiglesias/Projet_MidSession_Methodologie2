using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] SceneData sceneToLoad;
    public void OnStartClick()
    {
        SceneManager.LoadScene(sceneToLoad.sceneName);
    }
}
