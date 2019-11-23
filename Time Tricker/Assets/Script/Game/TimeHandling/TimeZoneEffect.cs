using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Used in an object with tag "TimeZone", stores the value of time dillation
 * which is used by TimeManager when another object is collinding
 * exemple : power of the player, or a special zone where time is slowed down
 */
public class TimeZoneEffect : MonoBehaviour
{
    public float timeEffect = 1f;
}