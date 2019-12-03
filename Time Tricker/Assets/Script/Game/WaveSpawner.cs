using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [System.Serializable]
    public class EnemyType
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    [System.Serializable]
    public class Wave
    {
        public string name;
        public EnemyType[] enemyType;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 0.5f;
    public GameObject spawnEffect;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    private ScoreUpdate su;

    private void Start()
    {
        su = GameObject.FindObjectOfType<ScoreUpdate>();
        waveCountdown = timeBetweenWaves;
        if(spawnPoints.Length == 0)
        {
            Debug.LogError("Pas de points de spawn");
        }
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        su.addScore(1);

        if(nextWave + 1 > waves.Length -1)
        {
            state = SpawnState.WAITING;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().gameHasWon = true; 
            Debug.Log("All wave completed");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }   
        }
        return true;
    }

    IEnumerator SpawnWave (Wave p_wave)
    {
        Debug.Log("Spwaning wave : " + p_wave.name);
        state = SpawnState.SPAWNING;

        for(int enemyTypeIndex = 0; enemyTypeIndex < p_wave.enemyType.Length; enemyTypeIndex++)
        {
            for(int countOfEnemy = 0; countOfEnemy < p_wave.enemyType[enemyTypeIndex].count; countOfEnemy++)
            {
                SpawnEnemy(p_wave.enemyType[enemyTypeIndex].enemy);
                yield return new WaitForSeconds(1.0f / p_wave.enemyType[enemyTypeIndex].rate);
            }
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform p_enemy)
    {
        Debug.Log("Enemy spawn ");
        Transform l_swaningPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(p_enemy, l_swaningPoint.position, l_swaningPoint.rotation);
        //create pawn effect
        Instantiate(spawnEffect, l_swaningPoint.position, Quaternion.identity);
        //play sound
        GetComponent<SoundManagerSpawner>().PlaySpawn();
    }
}
