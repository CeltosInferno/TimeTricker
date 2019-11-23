using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTimeManager : TimeManager
{
    TimeEntity timeEntity;
    Animator anim;
    Rigidbody2D rb;
    //the default value of the gravity from the RigidBody,
    //read at the begining, if modified by another script, 
    //this one needs to be informed
    float rb_gravityScale;

    //the name of the value that defines the speed of the Animator
    public string animatorTimeName = "timeSpeed";

    void Start()
    {
        timeEntity = GetComponent<TimeEntity>();
        if (timeEntity == null) Debug.LogError("Could not find a TimeEntity in BasicTimeManager");
        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("Could not find an Animator in BasicTimeManager");
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Could not find a Rigidbody2D in BasicTimeManager");
        else rb_gravityScale = rb.gravityScale;
    }

    void normalReaction(float value)
    {
        if (timeEntity != null) timeEntity.SetTimeScale(value);
        if (anim != null) anim.SetFloat(animatorTimeName, value);
        if (rb != null) rb.gravityScale = rb_gravityScale * value;
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
