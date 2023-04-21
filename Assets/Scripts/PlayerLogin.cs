using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerLogin : MonoBehaviour
{

    [SerializeField] TMP_InputField usernameInput;
    string username;
    [SerializeField] TMP_InputField passwordInput;
    string password;
    [SerializeField] GameObject invalidUser;
    [SerializeField] GameObject notVerified;
    [SerializeField] SceneData UserMenuScene;
    //string userID;
    string userIDtext;

    public void OnClick()
    {
        username = usernameInput.text;
        password = passwordInput.text;

        StartCoroutine(LoadPlayer());

    }

    public void NoLoginClick()
    {
        BackUP userdata = Persistance.LoadData(GameManager.Instance.userData.username);
        if(userdata != null)
        {
            //Character.Instance.LoadBackup(userdata);
            GameManager.Instance.userData = userdata;
            SceneManager.LoadScene(UserMenuScene.sceneName);
        }
    }

    public IEnumerator LoadPlayer()
    {
        string uri = "https://parseapi.back4app.com/login?username="+username+"&password="+password;
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                CodeError errorCode = JsonUtility.FromJson<CodeError>(request.downloadHandler.text);
                if (errorCode.code == 205)
                {
                    notVerified.SetActive(true);
                    
                }
                if (errorCode.code == 101)
                {
                    invalidUser.SetActive(true);
                }

                //Debug.LogError(request.error);
                //Debug.LogError(errorCode.code);
                yield break;
            }
            Debug.Log(request.downloadHandler.text);
            UserIdCheck userID = JsonUtility.FromJson<UserIdCheck>(request.downloadHandler.text);
            userIDtext = userID.objectId;
            Debug.Log(userIDtext);
            StartCoroutine(CheckExist());


        }
    }

    public IEnumerator CheckExist()
    {
        string uri = "https://parseapi.back4app.com/classes/BackUp/?where={\"LoginId\":\"" + userIDtext + "\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;

            }

            Debug.Log(request.downloadHandler.text);
            BackUPResults backups = JsonUtility.FromJson<BackUPResults>(request.downloadHandler.text);

            if (backups.results.Length == 0)
            {
                StartCoroutine(CreatePlayerBackUp());
            }
            else
            {
                GameManager.Instance.userData = backups.results.First();
                SceneManager.LoadScene(UserMenuScene.sceneName);
            }
        }
    }

    public IEnumerator CreatePlayerBackUp()
    {
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/BackUp", "POST"))
        {
            var json = "{\"LoginId\": \"" + userIDtext + "\",\"username\": \"" + username + "\", \"MaxHealth\" : 1000, \"PowerAttack\" : 1, \"Gold\" : 0, \"MonstersKilled\" : 0, \"GameOver\" : 0}";
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;
            }
            Persistance.SaveData(json, userIDtext);
            GameManager.Instance.userData = JsonUtility.FromJson<BackUP>(json);
            SceneManager.LoadScene(UserMenuScene.sceneName);
            Debug.Log(request.downloadHandler.text);
        }
    }
}
