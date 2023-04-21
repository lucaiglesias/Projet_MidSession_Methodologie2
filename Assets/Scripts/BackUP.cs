using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BackUP
{
    public string objectId;
    public string updatedA;
    public string createdAt;
    [NonSerialized] public string ACL;
    public string LoginId;
    public string username;
    public int MaxHealth;
    public int PowerAttack;
    public int Gold;
    public int MonstersKilled;
    public int GameOver;

    public BackUP()
    {
        username = "Local";
        MaxHealth = 1000;
        PowerAttack = 1;
        Gold = 0;
        MonstersKilled = 0;
        GameOver = 0;
    }


}

[Serializable]
public class BackUPResults
{
    public BackUP[] results;
}

