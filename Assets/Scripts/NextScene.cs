using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string LevelName;

    public void OnClick()
    {
        var progress = SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Single);
    }
}
