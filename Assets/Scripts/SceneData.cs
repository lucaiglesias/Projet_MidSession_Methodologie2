using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//based on class exercice
public class SceneData : ScriptableObject
{
    public string sceneName;

#if UNITY_EDITOR
    public UnityEditor.SceneAsset sceneAsset;
    public UnityEditor.SceneAsset Scene
    {
        get => sceneAsset;
        set
        {
            sceneAsset = value;
            sceneName = sceneAsset.name;
        }
    }
#endif
}


