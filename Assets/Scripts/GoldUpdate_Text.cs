using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GoldUpdate_Text : MonoBehaviour
{
    [SerializeField] private TMP_Text Gold_Status;
    //[SerializeField] private GameObject textLocalizer;


    void Start()
    {

        //lvl_Status.text = "Level : " + Character.Instance.lvl;
        //lvl_Status.text = GetComponent<TMP_Text>().text + " : " +  Character.Instance.lvl;
        Gold_Status.text = Gold_Status.text + " : " + Character.Instance.Gold;

    }

    private void Update()
    {
        //lvl_Status.text = "Level : " + Character.Instance.lvl;
        //lvl_Status.text = GetComponent<TMP_Text>().text + " : " + Character.Instance.lvl;
        Gold_Status.text = Gold_Status.text + " : " + Character.Instance.Gold;
    }

}