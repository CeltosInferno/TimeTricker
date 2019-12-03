using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTimeManager : TimeManager
{

    TimeEntity timeEntity;
    Animator anim;

    //the name of the value that defines the speed of the Animator
    public string animatorTimeName = "timeSpeed";

    // Start is called before the first frame update
    void Start()
    {
        timeEntity = GetComponent<TimeEntity>();
        if (timeEntity == null) Debug.LogError("Could not find a TimeEntity in BasicTimeManager");
        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("Could not find an Animator in BasicTimeManager");
    }

    void normalReaction(float value)
    {
        if (timeEntity != null) timeEntity.SetTimeScale(value);
        if (anim != null) anim.SetFloat(animatorTimeName, value);
    }

    override public void ReactToSpeedUp(float value)
    {
        normalReaction(1f / value);
    }
    public override void ReactToSlowDown(float value)
    {
        normalReaction(1f / value);
    }
    public override void ReactNormalState()
    {
        normalReaction(1.0f);
    }

    public override void _Update()
    { }
}
