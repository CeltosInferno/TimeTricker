using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Represent a "reversed" behaviour of TimeEntities when 
 * time is affected, they slow down when time is speeded up
 * they speed up when it decelerates...
 */
public class ReverseTimeManager : BasicTimeManager
{
    //in order to avoid unwanted strong forces
    //some limits are set to the reaction to the time
    //(no speed x10) which would break the game
    public float minSlow = 0.05f;
    public float maxSpeed = 3f;

    protected override void normalReaction(float value) {
        value = Mathf.Max(Mathf.Min(1f / value, maxSpeed), minSlow);
        base.normalReaction(value);
    }
}
