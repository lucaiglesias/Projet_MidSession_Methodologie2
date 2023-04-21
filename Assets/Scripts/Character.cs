using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
    //based on https://www.youtube.com/watch?v=ixSN42SM3gQ&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=1

    public string username = "Local";
    public int maxHp = 1000;
    public int currentHp = 1000;
    public int lvl = 1;
    public int lvlBar = 0;
    public int maxLvlBar = 100;
    public int Gold;
    public int PowerAttack;
    public int MonstersKilled;
    public int GameOver;
    [SerializeField] HealthBar healthBar;
    [SerializeField] LvlBar lvlbar;


    public static Character Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        LoadBackup(GameManager.Instance.userData);

    }

    private void Start()
    {
        lvlbar.SetState(lvlBar, maxLvlBar);
    }


    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            Debug.Log("Character is dead");
            GameManager.Instance.GameOver();
        }

        healthBar.SetState(currentHp, maxHp);
        
    }

    public void Heal(int amount)
    {
        if(currentHp <= 0) { return; }

        currentHp += amount;
        if(currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void LevelUp(int lvlup)
    {
        lvlBar += lvlup;

        if(lvlBar >= maxLvlBar)
        {
            lvl++;
            lvlBar -= maxLvlBar;
            maxLvlBar += 100;
            
        }
        lvlbar.SetState(lvlBar, maxLvlBar);
    }


    public void LoadBackup(BackUP backupData)
    {
        username = backupData.username;
        maxHp = backupData.MaxHealth;
        PowerAttack = backupData.PowerAttack;
        Gold = backupData.Gold;
        MonstersKilled = backupData.MonstersKilled;
        GameOver = backupData.GameOver; 
        
    }

}
