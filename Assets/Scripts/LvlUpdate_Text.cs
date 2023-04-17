using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LvlUpdate_Text : MonoBehaviour
{
    [SerializeField] private TMP_Text lvl_Status;
    //[SerializeField] private GameObject textLocalizer;


    void Start()
    {

        //lvl_Status.text = "Level : " + Character.Instance.lvl;
        //lvl_Status.text = GetComponent<TMP_Text>().text + " : " +  Character.Instance.lvl;
        lvl_Status.text = lvl_Status.text + " : " + Character.Instance.lvl;

    }

    private void Update()
    {
        //lvl_Status.text = "Level : " + Character.Instance.lvl;
        //lvl_Status.text = GetComponent<TMP_Text>().text + " : " + Character.Instance.lvl;
        lvl_Status.text = lvl_Status.text + " : " + Character.Instance.lvl;
    }

}
