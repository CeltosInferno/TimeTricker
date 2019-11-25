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

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void addScore(int sc)
    {
        StartCoroutine(resizeFont());
        text.text = (System.Convert.ToInt32(text.text) + sc).ToString();
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
