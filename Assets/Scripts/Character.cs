using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHp = 1000;
    public int currentHp = 1000;
    public int lvl = 1;
    public int lvlBar;
    public int maxLvlBar = 100;
    [SerializeField] HealthBar healthBar;

    public static Character Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if(currentHp <= 0)
        {
            Debug.Log("Character is dead");
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
        }
    }
}
