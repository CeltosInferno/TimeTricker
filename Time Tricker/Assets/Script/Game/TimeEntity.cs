using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Represent an abstract script which depends on
 * time modification, should be referenced by a TimeManager object
 * timeScale should be used in children classes in calculs
 * Exemple : enemy, player, plateform, bullet
 */
public abstract class TimeEntity : MonoBehaviour
{
    protected float m_timeScale = 1f;
    public void SetTimeScale(float value)
    {
        m_timeScale = value;
    }

}

