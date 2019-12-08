using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    /*
     * Defines when the status of the wave :
     * SPAWNING : creating the enemies
     * WAITING : wating for the wave to end
     * COUNTING : counting time until next wave
     */
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    /*
     * Defines when the new wave should start :
     * LASTSPAWN : when the last enemy appears
     * LASTKILL : when the last enemy dies
     */
    public enum WaveWaitMode
    {
        LASTSPAWN,
        LASTKILL
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
        //gives a time until the next wave is to be computed
        //if 0f, uses th default value from "timeBetweenWaves" 
        public float timeToHandle = 0f;
        public int scoreOnFinished = 5;
    }

    public Wave[] waves;
    //represent the current wave (paradoxaly)
    private int nextWave = 0;

    public Transform[] spawnPoints;

    //time before the first wave
    public float timeFirstWave = 3f;
    //default time between waves (in seconds)
    public float timeBetweenWaves = 0.5f;
    public GameObject spawnEffect;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    public WaveWaitMode waveWait = WaveWaitMode.LASTSPAWN;

    //used in order to display the time unti next wave
    private Chrono m_chrono;
    private ScoreUpdate su;

    private void Start()
    {
        su = GameObject.FindObjectOfType<ScoreUpdate>();
        waveCountdown = timeFirstWave;
        m_chrono = GameObject.FindGameObjectWithTag("WaveChrono").GetComponent<Chrono>();
        if (m_chrono == null)
            Debug.LogError("Could not find any Chrono object with tag \"Chrono\" in script WaveSpawner.cs");
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Pas de points de spawn");
        }
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (WaveIsOver())
            {
                WaveCompleted();
            }
            //else return;
        }

        if (state == SpawnState.COUNTING)
        {
            CountingForNextWave();
        }
    }

    //waiting for the end of the current wave
    public bool WaveIsOver()
    {
        //if we expect the wave to end right after the last spawn
        if (waveWait == WaveWaitMode.LASTSPAWN)
        {
            //we wait for the end of the game
            if (isLastWave()) return !EnemyIsAlive();
            else return true; //else we directly launch the chrono
        }

        //if we expect the wave to end after there is no enemy left
        if (waveWait == WaveWaitMode.LASTKILL)
        {
            return !EnemyIsAlive();
        }

        return false; //should not occur
    }

    //count the time and displays it until
    //the next wave starts
    public void CountingForNextWave()
    {
        //If countdown is over, we play the new wave music and start the monster spawns
        if (waveCountdown <= 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SoundManagerGlobal>().NewWaveMusic();
            StartCoroutine(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFlashAndShake>().FlashAndShake(1.5f, 2.0f));
            StartCoroutine(SpawnWave(waves[nextWave]));
        }
        else
        {        

            waveCountdown = Mathf.Max(waveCountdown - Time.deltaTime, 0f);
            m_chrono.setTimeText(waveCountdown);
        }
    }

    bool isLastWave()
    {
        return nextWave + 1 > waves.Length - 1;
    }

    //called when the current wave is considered over
    void WaveCompleted()
    {
        Debug.Log("Wave completed!");

        state = SpawnState.COUNTING;

        Wave current = waves[nextWave];

        su.addScore(current.scoreOnFinished);
        if (current.timeToHandle > 0f)
            waveCountdown = current.timeToHandle;
        else
            waveCountdown = timeBetweenWaves;

        //if last wave we display the end
        if(isLastWave())
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

    //return if there is still one enemy left
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
