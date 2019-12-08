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
    protected SoundManager soundManager;

    //the name of the value that defines the speed of the Animator
    public string animatorTimeName = "timeSpeed";

    //setting theses values above 0 allows to fix
    //a reaction to a time change
    public float basicNormalTime = -1f;
    public float basicSlowedTime = -1f;
    public float basicSpeededTime = -1f;

    protected virtual
        void Start()
    {
        timeEntity = GetComponent<TimeEntity>();
        if (timeEntity == null) Debug.LogError("Could not find a TimeEntity in BasicTimeManager");
        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("Could not find an Animator in BasicTimeManager");
        soundManager = GetComponent<SoundManager>();
        //if (soundManager == null) Debug.LogError("Could not find a SoundManager in BasicTimeManager");
        //SetPitch
    }

    protected virtual void normalReaction(float value)
    {
        if (timeEntity) timeEntity.SetTimeScale(value);
        if (anim) anim.SetFloat(animatorTimeName, value);
        if (soundManager) soundManager.SetPitch(value);
    }

    public override void ReactToSpeedUp(float value)
    {
        if (basicSpeededTime >= 0)
            normalReaction(basicSpeededTime);
        else
            normalReaction(value);
    }
    public override void ReactToSlowDown(float value)
    {
        if (basicSlowedTime >= 0)
            normalReaction(basicSlowedTime);
        else
            normalReaction(value);
    }
    public override void ReactNormalState()
    {
        if (basicNormalTime >= 0)
            normalReaction(basicNormalTime);
        else
            normalReaction(1f);
    }

    public override void _Update()
    { }
}
