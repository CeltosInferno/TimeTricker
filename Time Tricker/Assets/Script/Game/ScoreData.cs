using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public int[] scores;
    public string[] names;

    private string[] default_names = { "Pr.Xavier", "Pr.Layton", "Pr.Doofenshmirtz", "Pr.Einstein", "Pr.Potvin", "Pr.Tournesol", "Pr.Proton", "Pr.Snape" };

    public static void addScore(int new_score, string player_name)
    {
        ScoreData data = SaveSystem.LoadData();
        Debug.Log(data.scores[0]);
        //on vérifie que le score est bon
        if(new_score >= data.scores[7])
        {
            Debug.Log("Recording new score");
            //on localise la place où l'on doit enregistrer le score
            int i = 7;
            bool goodPlace = false;
            while(!goodPlace && i>0)
            {
                if(new_score >= data.scores[i - 1])
                {
                    i--;
                }
                else
                {
                    goodPlace = true;
                }
            }
            Debug.Log("Good place is : " + i);
            //On décale tous les scores
            int ToChangeScore;
            int ToPlaceScore = new_score;

            //et tous les pseudos
            string ToChangeName;
            string ToPlaceName = player_name;
            for (; i < 8; i++)
            {
                //Shuffling Scores
                ToChangeScore = data.scores[i];
                data.scores[i] = ToPlaceScore;
                ToPlaceScore = ToChangeScore;

                //Shuffling names
                ToChangeName = data.names[i];
                data.names[i] = ToPlaceName;
                ToPlaceName = ToChangeName;
            }
            Debug.Log("New scores : " + data.scores);
        }
        SaveSystem.SaveScore(data);
    }

    public ScoreData()
    {
        scores = new int[8];
        names = new string[8];
        //loading previous scores
        for (int i=0; i < 8; i++)
        {
            scores[i] = (8-i) * 5;
            names[i] = default_names[i];
        }
    }

}
