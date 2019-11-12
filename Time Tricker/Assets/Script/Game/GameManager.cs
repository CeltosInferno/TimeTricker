using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject spawner;
    public float delayRestart;
    public float delayDisplayRound;

    bool gameHasEnded = false;

    private void Awake()
    {
        gameOver.SetActive(false);
    }
    private void Update()
    {
        if(spawner.transform.childCount == 0)
        {
            WinGame();
        }

        if (GameObject.FindGameObjectsWithTag("Hud")[0].GetComponent<PlayerHealth>().gameOver)
        {
            EndGame();
        }

        //return to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("------ Game Over -------");
            gameOver.SetActive(true);

            Invoke("GameOver", 0);
            Invoke("Restart", delayRestart);
        }
    }

    public void WinGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("------ Win -------");
            gameOver.SetActive(true);

            Invoke("NextRound", delayDisplayRound);
            Invoke("Restart", delayRestart);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NextRound()
    {
        gameOver.GetComponentInChildren<Text>().text = "Next round";
        gameOver.GetComponentInChildren<Text>().color = Color.green;
    }

    void GameOver()
    {
        gameOver.GetComponentInChildren<Text>().text = "Game Over";
        gameOver.GetComponentInChildren<Text>().color = Color.red;
    }
}
