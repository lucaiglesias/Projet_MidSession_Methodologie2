using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LvlUpdate_Text : MonoBehaviour
{
    [SerializeField] private TMP_Text lvl_Status;


    // Start is called before the first frame update
    void Start()
    {
        lvl_Status.text = "Level : " + Character.Instance.lvl;
    }

    private void Update()
    {
        lvl_Status.text = "Level : " + Character.Instance.lvl;
    }

}
