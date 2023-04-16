using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms;

public class Localization : MonoBehaviour
{
    static string pick = "en";
    //[SerializeField] string key;

    private static IDictionary<string, IDictionary<string, string>> localization = new Dictionary<string, IDictionary<string, string>>();



    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            pick = "en";
            Debug.Log("English selected");
        }
        if (val == 1)
        {
            pick = "es";
            Debug.Log("Spanish selected");
        }
        if (val == 2)
        {
            pick = "pt";
            Debug.Log("Portuguese selected");
        }
        if (val == 3)
        {
            pick = "fr";
            Debug.Log("French selected");
        }
        if (val == 4)
        {
            pick = "it";
            Debug.Log("Italian selected");
        }

        if (string.IsNullOrEmpty(pick)) {
            pick = "en";    
        }


        
    }


    public static string GetMessage(string key)
    {
        return localization[pick][key];
    }



    public static void Localizer()
    {
        //Linha
        string[] languages = File.ReadAllLines("C:\\Users\\luca_\\source\\repos\\Localization\\Localization\\dicionario.csv");


        //Coluna
        string[] keys = languages[0].Split(';');


        //Create dictionary area


        //
        for (int i = 1; i < keys.Length; i++)
        {
            localization.Add(keys[i], new Dictionary<string, string>());

            for (int j = 1; j < languages.Length; j++)
            {
                string[] language = languages[j].Split(';');
                localization[keys[i]].Add(language[0], language[i]);
            }
        }

        Debug.Log("iniciou");


    }
}
