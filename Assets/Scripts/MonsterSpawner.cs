using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    public bool monsterAlive;

    public static event Action mobSpawned;

    [SerializeField] GameObject boar, snake, bat;

    private void Start()
    {
        monsterAlive = false;
    }

    private void Update()
    {
        Monster.mobDied += MonsterDied;

        if (!monsterAlive)
        {
            StartCoroutine(SpawnMonster());
        }

        
    }
    IEnumerator SpawnMonster()
    {
        monsterAlive = true;

        GameObject[] monsters = { boar, snake, bat };

        int rndNum = Random.Range(0, 3);

        GetComponent<Animator>().SetTrigger("TrArrival");

        yield return new WaitForSeconds(0.05f);

        GameObject spawnedMonster = Instantiate(monsters[rndNum], transform.position, Quaternion.identity);

        spawnedMonster.transform.parent = gameObject.transform;

        mobSpawned.Invoke();
    }

    void MonsterDied()
    {
        monsterAlive = false;
    }
}
