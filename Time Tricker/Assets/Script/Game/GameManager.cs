using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverDisplay;
    public GameObject spawner;

    public float delayRestart;
    public float delayDisplayRound;

    public bool gameHasEnded;

    private void Start()
    {
        gameHasEnded = false;
    }
    private void Awake()
    {
        gameOverDisplay.SetActive(false);
    }
    private void Update()
    {
        if (gameHasEnded)
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
        Debug.Log("------ Game Over -------");
        gameOverDisplay.SetActive(true);

        Invoke("GameOver", 0);
        Invoke("Restart", delayRestart);
    }

    public void WinGame()
    {
        Debug.Log("------ Win -------");
        gameOverDisplay.SetActive(true);

        Invoke("NextRound", delayDisplayRound);
        Invoke("Restart", delayRestart);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NextRound()
    {
        gameOverDisplay.GetComponentInChildren<Text>().text = "Next round";
        gameOverDisplay.GetComponentInChildren<Text>().color = Color.green;
    }

    void GameOver()
    {
        gameOverDisplay.GetComponentInChildren<Text>().text = "Game Over";
        gameOverDisplay.GetComponentInChildren<Text>().color = Color.red;
    }
}
