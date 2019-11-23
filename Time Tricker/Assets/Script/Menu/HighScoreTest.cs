using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreTest : MonoBehaviour
{
    public int my_score = 14;
    public string my_name = "Laura";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ScoreData.addScore(my_score,my_name);
        }
    }
}
