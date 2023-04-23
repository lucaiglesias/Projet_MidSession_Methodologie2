using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Text;

public class CouponValidation : MonoBehaviour
{
    [SerializeField] TMP_InputField codeInput;
    string code;
    [SerializeField] GameObject SucessCode;
    [SerializeField] GameObject UsedCode;
    [SerializeField] GameObject SameTypeCode;
    [SerializeField] GameObject DoesntExistCode;
    Coupon[] userCoupons = null;

    public void OnClick()
    {
        code = codeInput.text;
        StartCoroutine(CheckCode());
    }

    public IEnumerator CheckCode()
    {
        string uri = "https://parseapi.back4app.com/classes/Coupon?where={\"Code\":\"" + code + "\"}";
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
            CouponResults codeReturn = JsonUtility.FromJson<CouponResults>(request.downloadHandler.text);
            bool typeRedeemed = false;
            if (codeReturn.results.Length == 0)
            {
                DoesntExistCode.SetActive(true);
            }
            else if (!codeReturn.results.First().Redeemed)
            {
                Coupon coupon = codeReturn.results.First();
                StartCoroutine(GetUserCoupons(GameManager.Instance.loggedUser.objectId));
                if (userCoupons != null)
                {

                    for (int i = 0; i < userCoupons.Length; i++)
                    {
                        if (coupon.Type.Equals(userCoupons[i].Type))
                        {
                            typeRedeemed = true;
                            break;
                        }

                    }

                }
                if (typeRedeemed)
                {
                    SameTypeCode.SetActive(true);
                    yield break;
                }
                else
                {



                    if (coupon.Type == "A")
                    {
                        GameManager.Instance.characterData.Gold += 10;
                    }
                    if (coupon.Type == "B")
                    {
                        GameManager.Instance.characterData.MaxHealth += 50;
                    }
                    if (coupon.Type == "C")
                    {
                        GameManager.Instance.characterData.PowerAttack += 5;
                    }
                    coupon.Redeemed = true;
                    coupon.userId = GameManager.Instance.loggedUser.objectId;
                    SucessCode.SetActive(true);
                    StartCoroutine(UpdateCoupon(coupon));
                    Persistance.SaveData(GameManager.Instance.characterData, coupon.userId);
                    StartCoroutine(PlayerLogin.UpdatePlayerBackUp(GameManager.Instance.characterData));



                }
            }
            else
            {
                UsedCode.SetActive(true);
            }
        }
    }

    public IEnumerator UpdateCoupon(Coupon coupon)
    {
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/Coupon/" + coupon.objectId, "PUT"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            request.SetRequestHeader("Content-Type", "application/json");
            var json = JsonUtility.ToJson(coupon);
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;

            }

            Debug.Log(request.downloadHandler.text);
        }
    }
    public IEnumerator GetUserCoupons(string userId)
    {
        userCoupons = null;
        string uri = "https://parseapi.back4app.com/classes/Coupon?where={\"userId\":\"" + userId + "\"}";
        using (var request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                yield break;

            }

            Debug.Log(request.downloadHandler.text);
            CouponResults codeReturn = JsonUtility.FromJson<CouponResults>(request.downloadHandler.text);
            userCoupons = codeReturn.results;


        }
    }
}
