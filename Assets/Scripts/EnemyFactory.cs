using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    //Based on class

    //SINGLETON
    private static EnemyFactory _instance;
    public static EnemyFactory Instance { get { return _instance; } }

    //Create List of enemies
    private List<GameObject> enemiesLvl1 = new List<GameObject>();
    private List<GameObject> enemiesLvl2 = new List<GameObject>();
    private List<GameObject> enemiesLvl3 = new List<GameObject>();

    //Set manually maximum size of list for each enemy
    [SerializeField] private int maxEnemiesLvl1;
    [SerializeField] private int maxEnemiesLvl2;
    [SerializeField] private int maxEnemiesLvl3;

    //Set enemies on Unity
    [SerializeField] private GameObject enemyLvl1;
    [SerializeField] private GameObject enemyLvl2;
    [SerializeField] private GameObject enemyLvl3;

    private void Awake()
    {
        if (_instance== null)
        {
            _instance = this;
        }
        else if(_instance != null && _instance != this)
        {
            Destroy(_instance);
        }
    }

    //Creat list of enemies and put in an Object Pool
    private void Start()
    {
        GameObject newLvl1Enemy;
        GameObject newLvl2Enemy;
        GameObject newLvl3Enemy;

        for(int i = 0; i < maxEnemiesLvl1; i++)
        {
            newLvl1Enemy = Instantiate(enemyLvl1);
            enemiesLvl1.Add(newLvl1Enemy);
            newLvl1Enemy.SetActive(false);
        }
        for (int i = 0; i < maxEnemiesLvl2; i++)
        {
            newLvl2Enemy = Instantiate(enemyLvl2);
            enemiesLvl2.Add(newLvl2Enemy);
            newLvl2Enemy.SetActive(false);
        }
        for (int i = 0; i < maxEnemiesLvl3; i++)
        {
            newLvl3Enemy = Instantiate(enemyLvl3);
            enemiesLvl3.Add(newLvl3Enemy);
            newLvl3Enemy.SetActive(false);
        }
    }

    //Call each enemy from the pool
    public GameObject CreateLvl1Enemy()
    {
        GameObject spawnedEnemy = new GameObject();
        for (int i = 0; i < enemiesLvl1.Count; i++)
        {
            if (!enemiesLvl1[i].activeInHierarchy)
            {
                enemiesLvl1[i].SetActive(true);
                spawnedEnemy = enemiesLvl1[i];
                break;
            }
        }
        return spawnedEnemy;

    }

    public GameObject CreateLvl2Enemy()
    {
        GameObject spawnedEnemy = new GameObject();
        for (int i = 0; i < enemiesLvl2.Count; i++)
        {
            if (!enemiesLvl2[i].activeInHierarchy)
            {
                enemiesLvl2[i].SetActive(true);
                spawnedEnemy = enemiesLvl2[i];
                break;
            }
        }
        return spawnedEnemy;

    }

    public GameObject CreateLvl3Enemy()
    {
        GameObject spawnedEnemy = new GameObject();
        for (int i = 0; i < enemiesLvl3.Count; i++)
        {
            if (!enemiesLvl3[i].activeInHierarchy)
            {
                enemiesLvl3[i].SetActive(true);
                spawnedEnemy = enemiesLvl3[i];
                break;
            }
        }
        return spawnedEnemy;

    }


}
