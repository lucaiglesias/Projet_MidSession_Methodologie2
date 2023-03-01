#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


//based on class exercice
public class EditorMenu : MonoBehaviour
{
    [MenuItem("Tool/Update Scene Assets")]
    static void UpdateSceneAssets() 
    { 
        if (!AssetDatabase.IsValidFolder(Path.Combine("Assets", "ScriptableObjects")))
        { AssetDatabase.CreateFolder("Assets", "ScriptableObjects"); } 
        
        foreach (var sceneAsset in GetSceneAssets()) 
        { 
            var sceneData = ScriptableObject.CreateInstance<SceneData>(); 
            sceneData.Scene = sceneAsset; 
            string assetPath = Path.Combine("Assets", "ScriptableObjects", sceneData.sceneName + ".asset"); 
            AssetDatabase.CreateAsset(sceneData, assetPath); 
        } 
    }


    static string[] searchInFolders = new[] { "Assets/Scenes/" };

    static List<SceneAsset> GetSceneAssets()
    {
        string[] sceneGuids = AssetDatabase.FindAssets("t:SceneAsset", searchInFolders);
        var sceneAssets = new List<SceneAsset>();
        foreach (var sceneGuid in sceneGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            sceneAssets.Add(AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath));
        }
        return sceneAssets;
    }
}

#endif