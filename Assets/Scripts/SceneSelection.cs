using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelection : MonoBehaviour
{
    private AsyncOperation async;
    
    public void LoadNextScene(string name)
    {
        if(async == null)
        {
            async = SceneManager.LoadSceneAsync(name);
        }
    }
}
