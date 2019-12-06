using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTimeManager : BasicTimeManager
{
    public float minSlow = 0.05f;
    public float maxSpeed = 3f;

    override public void ReactToSpeedUp(float value)
    {
        normalReaction(Mathf.Min(1f / value, maxSpeed));
    }
    public override void ReactToSlowDown(float value)
    {
        normalReaction(Mathf.Max(1f / value, minSlow));
    }
    public override void ReactNormalState()
    {
        normalReaction(1.0f);
    }
}
