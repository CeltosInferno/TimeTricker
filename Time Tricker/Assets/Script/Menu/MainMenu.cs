using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelBuilding");
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void SaveScore()
    {
        TMP_InputField TMP = GameObject.FindObjectOfType<TMP_InputField>();
        ScoreData.addScore(ScoreUpdate.getScore(), "Pr." + TMP.text);
    }
}
