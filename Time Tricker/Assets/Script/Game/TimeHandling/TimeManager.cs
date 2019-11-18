using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * TimeManager Class should represent how an object interpret
 * a time modification, its children classes overrides how the 
 * reaction operates
 */
public abstract class TimeManager : MonoBehaviour
{
    //static ?
    public static float globalTimeMultiplier = 1f;
    public float timeMultiplier = 1f;

    public abstract void ReactToSlowDown(float value);
    public abstract void ReactToSpeedUp(float value);
    public abstract void ReactNormalState();

    //called once per frame with the multiplier being how it is affected
    //mult is the coefficient of affectation : 
    //1f if no affectation
    //below 1f if slow down
    //above it if speed up
    public void ReactToTime(float mult)
    {
        //Debug.Log(mult);
        if (mult > 1f)
            ReactToSpeedUp(mult);
        else if (mult < 1f)
            ReactToSlowDown(mult);
        else
            ReactNormalState();
    }

    //might be unneeded
    public abstract void _Update();

    //when colliding with a timeZone : add its time modifier
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision !");
        if (collision.tag == "TimeZone")
        {
            Debug.Log("Collision2 !");
            TimeZoneEffect timeZone = collision.GetComponent<TimeZoneEffect>();
            if (timeZone != null && timeZone.timeEffect > 0)
            {
                timeMultiplier *= timeZone.timeEffect;
            }
        }
    }

    //when ending collsion with a timeZone : remove its time modifier
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TimeZone")
        {
            TimeZoneEffect timeZone = collision.GetComponent<TimeZoneEffect>();
            if (timeZone != null && timeZone.timeEffect > 0)
            {
                timeMultiplier /= timeZone.timeEffect;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _Update();
        ReactToTime(globalTimeMultiplier * timeMultiplier);
    }
}