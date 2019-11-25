using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour
{

    Text text;
    int delta = 1;
    int maxFontSize = 27;
    int minFontSize = 15;
    private static int score;

    void Start()
    {
        text = GetComponent<Text>();
        addScore(0);
    }

    public void addScore(int sc)
    {
        StartCoroutine(resizeFont());
        score += sc;
        text.text = score.ToString();
    }

    public IEnumerator resizeFont()
    {
        text.fontSize = System.Math.Min(text.fontSize + delta, maxFontSize);
        yield return new WaitForSeconds(1);
        text.fontSize = System.Math.Max(text.fontSize - delta, minFontSize);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            addScore(1);
            Debug.Log("ADDING SCORE");
        }
    }
}
