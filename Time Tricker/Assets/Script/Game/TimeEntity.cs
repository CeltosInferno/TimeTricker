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

    private Rigidbody2D m_rb;

    public virtual
        void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    public void SetTimeScale(float value)
    {
        if (value <= 0f)
            value = 0.000001f;
        float old_timeScale = m_timeScale;
        m_timeScale = value;
        if (m_rb)
        {
            float ratio = m_timeScale / old_timeScale;
            m_rb.velocity *= ratio;
            m_rb.angularDrag *= ratio;
            m_rb.angularVelocity *= ratio;
            m_rb.drag *= ratio;
            m_rb.gravityScale *= ratio;
            m_rb.mass *= ratio;
        }
    }

    public void TimeTranslate(Transform transform, Vector3 value)
    {
        transform.Translate(value * m_timeScale);
    }

    public void TimeAddForce(Rigidbody2D rb, Vector2 force, ForceMode2D mode)
    {
        rb.AddForce(force * m_timeScale, mode);
    }

}

