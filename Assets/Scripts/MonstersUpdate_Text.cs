using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MonstersUpdate_Text : MonoBehaviour
{
    [SerializeField] private TMP_Text MonstersKilled_Status;
    //[SerializeField] private GameObject textLocalizer;


    void Start()
    {

        //lvl_Status.text = "Level : " + Character.Instance.lvl;
        //lvl_Status.text = GetComponent<TMP_Text>().text + " : " +  Character.Instance.lvl;
        MonstersKilled_Status.text = MonstersKilled_Status.text + " : " + Character.Instance.MonstersKilled;

    }

    private void Update()
    {
        //lvl_Status.text = "Level : " + Character.Instance.lvl;
        //lvl_Status.text = GetComponent<TMP_Text>().text + " : " + Character.Instance.lvl;
        MonstersKilled_Status.text = MonstersKilled_Status.text + " : " + Character.Instance.MonstersKilled;
    }

}