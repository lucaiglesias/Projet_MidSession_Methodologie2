using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextLocalizer : MonoBehaviour
{
    [SerializeField] string key;

    public void Localize()
    {
        GetComponent<TMP_Text>().text = Localization.GetMessage(key);
    }

    public void Update()
    {
        Localize();
    }
}
