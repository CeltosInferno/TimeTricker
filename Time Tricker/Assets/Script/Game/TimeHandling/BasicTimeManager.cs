using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Represent the default behaviour of TimeEntities when 
 * time is affected, they slow down when time is slow down
 * they speed up when it accelerates...
 */
public class BasicTimeManager : TimeManager
{
    protected TimeEntity timeEntity;
    protected Animator anim;

    //the name of the value that defines the speed of the Animator
    public string animatorTimeName = "timeSpeed";

    void Start()
    {
        timeEntity = GetComponent<TimeEntity>();
        if (timeEntity == null) Debug.LogError("Could not find a TimeEntity in BasicTimeManager");
        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("Could not find an Animator in BasicTimeManager");
    }

    protected void normalReaction(float value)
    {
        if (timeEntity != null) timeEntity.SetTimeScale(value);
        if (anim != null) anim.SetFloat(animatorTimeName, value);
    }

    override public void ReactToSpeedUp(float value)
    {
        normalReaction(value);
    }
    public override void ReactToSlowDown(float value)
    {
        normalReaction(value);
    }
    public override void ReactNormalState()
    {
        normalReaction(1f);
    }

    public override void _Update()
    { }
}
