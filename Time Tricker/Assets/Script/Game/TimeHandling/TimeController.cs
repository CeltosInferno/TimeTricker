using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script used to choose which button change the time and how
//Should be linked to the player and should have the gestion of energy in the future
public class TimeController : MonoBehaviour
{
    public float SlowValue = 0.5f;
    public float SpeedValue = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (TimeManager.globalTimeMultiplier > 1f) TimeManager.globalTimeMultiplier = 1f;
            else TimeManager.globalTimeMultiplier = SpeedValue;
        }
        //speed up button
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (TimeManager.globalTimeMultiplier < 1f) TimeManager.globalTimeMultiplier = 1f;
            else TimeManager.globalTimeMultiplier = SlowValue;
        }
        //clear state
        else if (Input.GetKeyDown(KeyCode.W))
        {
            TimeManager.globalTimeMultiplier = 1f;
        }
    }
}