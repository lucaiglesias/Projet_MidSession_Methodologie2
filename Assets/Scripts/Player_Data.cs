using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Linq;
using System;
using System.Net;

public class Player_Data : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    string username;
    [SerializeField] TMP_InputField passwordInput;
    string password;
    [SerializeField] TMP_InputField emailInput;
    string email;
    [SerializeField] GameObject wrongEmail;
    [SerializeField] GameObject rightUser;
    [SerializeField] GameObject wrongPassword;
    [SerializeField] GameObject sameUser;
    [SerializeField] GameObject sameEmail;


    


    public void OnClick()
    {
        username = usernameInput.text;
        password = passwordInput.text;
        email = emailInput.text;

        if (ValidateEmail(email))
        {
            StartCoroutine (CreatePlayer());

        }
        else
        {
            wrongEmail.SetActive(true);
        }
    }




    //based on https://www.c-sharpcorner.com/blogs/validate-email-address-in-c-sharp1
    private bool ValidateEmail(string checkEmail)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(checkEmail);
        if (match.Success)
            return true;
        else
            return false;
    }


    public IEnumerator CreatePlayer()
    {
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/users", "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            request.SetRequestHeader("Content-Type", "application/json");
            var json = "{\"username\": \"" + username + "\",\"password\": \""+password+"\"}";
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                CodeErrorTO errorCode = JsonUtility.FromJson<CodeErrorTO>(request.downloadHandler.text);
                if(errorCode.code == 202) 
                {
                    sameUser.SetActive(true);
                }
                if(errorCode.code == 203)
                {
                    sameEmail.SetActive(true);
                }

                //Debug.LogError(request.error);
                //Debug.LogError(errorCode.code);
                yield break;
            }
            Debug.Log(request.downloadHandler.text);
            rightUser.SetActive(true);
        }
    }




}
