using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
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

    public CharacterData()
    {
        username = "Local";
        MaxHealth = 100;
        PowerAttack = 1;
        Gold = 0;
        MonstersKilled = 0;
        GameOver = 0;
    }

    public CharacterData(string username)
    {

        if (username == null)
        {
            this.username = "Local";

        }
        else
        {
            this.username = username;
        }
        MaxHealth = 100;
        PowerAttack = 1;
        Gold = 0;
        MonstersKilled = 0;
        GameOver = 0;
    }


}

[Serializable]
public class CharacteDataResults
{
    public CharacterData[] results;
}

