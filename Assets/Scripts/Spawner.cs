using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Based on class
    [SerializeField] List<GameObject> spawnLocations = new List<GameObject>();
    GameObject spawn;
    int spawnIndex;

    public static Spawner Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    public void startCo()
    {

        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            spawnIndex = UnityEngine.Random.Range(0, spawnLocations.Count);
            spawn = spawnLocations[spawnIndex];

            EnemyFactory.Instance.CreateLvl1Enemy().transform.position = spawn.transform.position;

            if(Character.Instance.lvl >= 2)
            {
                EnemyFactory.Instance.CreateLvl2Enemy().transform.position = spawn.transform.position;
            }

            if (Character.Instance.lvl == 3)
            {
                EnemyFactory.Instance.CreateLvl3Enemy().transform.position = spawn.transform.position;
            }

            yield return new WaitForSeconds(2);
        }
    }

}
