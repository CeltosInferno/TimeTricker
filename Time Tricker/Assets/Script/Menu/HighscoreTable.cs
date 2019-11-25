using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private void Awake()
    {
        entryContainer = transform.Find("highscoreContainer");
        entryTemplate = entryContainer.Find("highscoreTemplate");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 20f;
        for (int i=0; i<8; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0,-templateHeight * i);
            entryTransform.gameObject.SetActive(true);


            ScoreData data = SaveSystem.LoadData();
            entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().text = (i+1).ToString();
            entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().text = data.scores[i].ToString();
            entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().text = data.names[i];
        }
    }
}
