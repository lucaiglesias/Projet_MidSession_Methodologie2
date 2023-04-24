using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    //based on https://www.youtube.com/watch?v=ixSN42SM3gQ&list=PL0GUZtUkX6t7zQEcvKtdc0NvjVuVcMe6U&index=1

    public string username = "";
    public int maxHp;
    public int currentHp;
    public int lvl;
    public int lvlBar = 0;
    public int maxLvlBar = 100;
    public int Gold;
    public int PowerAttack = 1;
    public int MonstersKilled;
    public int GameOver;
    [SerializeField] HealthBar healthBar;
    [SerializeField] LvlBar lvlbar;

    


    public static Character Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        LoadBackup(GameManager.Instance.characterData);
        currentHp = maxHp;

    }

    private void Start()
    {
    }

    public void Update()
    {
        if (lvl >= 2)
        {
            GameManager.Instance.NextLevel();
        }
    }

    public CharacterData getCharacterData()
    {
        var characterData = new CharacterData();
        characterData.username = username;
        characterData.MaxHealth = maxHp;
        characterData.PowerAttack = PowerAttack;
        characterData.Gold = Gold;
        characterData.MonstersKilled = MonstersKilled;
        characterData.GameOver = GameOver;
        return characterData;
    }



    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            GameOver++;
            Debug.Log("Character is dead");
            GameManager.Instance.GameOver();
        }

        healthBar.SetState(currentHp, maxHp);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Gold++;
            collision.gameObject.SetActive(false);
        }
    }


    public void Heal(int amount)
    {
        if (currentHp <= 0) { return; }

        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    public void LevelUp(int lvlup)
    {
        lvlBar += lvlup;

        if (lvlBar >= maxLvlBar)
        {
            PowerAttack++;
            currentHp = maxHp;

            lvl++;
            lvlBar -= maxLvlBar;
            maxLvlBar += 100;
            healthBar.SetState(currentHp, maxHp);

        }
        lvlbar.SetState(lvlBar, maxLvlBar);
    }


    public void LoadBackup(CharacterData backupData)
    {
        username = backupData.username;
        maxHp = backupData.MaxHealth;
        PowerAttack = backupData.PowerAttack;
        Gold = backupData.Gold;
        MonstersKilled = backupData.MonstersKilled;
        GameOver = backupData.GameOver;

        GameManager.Instance.characterData = backupData;


    }

}
