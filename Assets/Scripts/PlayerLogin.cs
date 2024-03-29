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

    //Login without Account
    public void NoLoginClick()
    {
        //if already exists
        CharacterData userdata = Persistance.LoadData(GameManager.Instance.characterData.username);
        if (userdata != null)
        {
            //Character.Instance.LoadBackup(userdata);
            GameManager.Instance.characterData = userdata;
        }
        //if doesnt exist
        else
        {
            CharacterData characterData = new CharacterData();
            Persistance.SaveData(characterData, characterData.username);
            GameManager.Instance.characterData = characterData;
        }
        SceneManager.LoadScene(UserMenuScene.sceneName);
    }


    //Login avec compte
    public IEnumerator LoadPlayer()
    {
        string uri = "https://parseapi.back4app.com/login?username=" + username + "&password=" + password;
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                //If account was not verified
                CodeError errorCode = JsonUtility.FromJson<CodeError>(request.downloadHandler.text);
                if (errorCode.code == 205)
                {
                    notVerified.SetActive(true);

                }
                //if account was not found
                if (errorCode.code == 101)
                {
                    invalidUser.SetActive(true);
                }

                //Debug.LogError(request.error);
                //Debug.LogError(errorCode.code);
                yield break;
            }
            Debug.Log(request.downloadHandler.text);
            UserIdCheck user = JsonUtility.FromJson<UserIdCheck>(request.downloadHandler.text);
            userIDtext = user.objectId;
            GameManager.Instance.loggedUser = user;
            Debug.Log(userIDtext);
            StartCoroutine(CheckExist());


        }
    }

    //Check if account already has a backup
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
            //if there's no data on their account
            Debug.Log(request.downloadHandler.text);
            CharacteDataResults backups = JsonUtility.FromJson<CharacteDataResults>(request.downloadHandler.text);

            if (backups.results.Length == 0)
            {
                StartCoroutine(CreatePlayerBackUp());
            }
            //if there's data on their account, load local file
            else
            {   
                CharacterData characterDataFile = Persistance.LoadData(backups.results.First().LoginId);
                if (characterDataFile != null)
                {
                    GameManager.Instance.characterData = characterDataFile;
                }
                //if there's no file, create a new file
                else
                {
                    GameManager.Instance.characterData = backups.results.First();
                    Persistance.SaveData(GameManager.Instance.characterData, GameManager.Instance.characterData.LoginId);

                }
                SceneManager.LoadScene(UserMenuScene.sceneName);
            }
        }
    }

    //Create data on backup
    public IEnumerator CreatePlayerBackUp()
    {
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/BackUp", "POST"))
        {
            var json = "{\"LoginId\": \"" + userIDtext + "\",\"username\": \"" + username + "\", \"MaxHealth\" : 100, \"PowerAttack\" : 1, \"Gold\" : 0, \"MonstersKilled\" : 0, \"GameOver\" : 0}";
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
            GameManager.Instance.characterData = JsonUtility.FromJson<CharacterData>(json);
            SceneManager.LoadScene(UserMenuScene.sceneName);
            Debug.Log(request.downloadHandler.text);
        }
    }

    //update backup
    public static IEnumerator UpdatePlayerBackUp(CharacterData characterData)
    {
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/BackUp/"+characterData.objectId, "PUT"))
        {
            var json = JsonUtility.ToJson(characterData);
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
        }
    }
}
