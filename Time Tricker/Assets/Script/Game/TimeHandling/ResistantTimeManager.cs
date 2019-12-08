using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * When the enemy is affected by the time, it changes its armor rate
 */
public class ResistantTimeManager : BasicTimeManager
{
    /*
     * Defines what the enemy is considered :
     * STRONG : when its resistance is below 1
     * WEAK : when its resistance is above 1
     * NORMAL : when its armor is at 1
     */
    public enum resistMode
    {
        STRONG,
        WEAK,
        NORMAL
    };


    public float resistanceWhenSlow = 1f;
    public float resistanceWhenNormal = 1f;
    public float resistanceWhenSpeed = 1f;

    public Color weakColor = Color.blue;
    public Color strongColor = Color.grey;

    private resistMode m_resistMode = resistMode.NORMAL;

    private Color[] baseColors;
    public SpriteRenderer[] sprites;

    private Enemy m_enemy;

    // Start is called before the first frame update
    protected override
        void Start()
    {
        base.Start();
        m_enemy = GetComponent<Enemy>();
        baseColors = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; ++i)
            baseColors[i] = sprites[i].color;
    }

    protected override void normalReaction(float value) {
        if (m_enemy)
        {
            if (m_enemy.resistanceRate < 1f)
                m_resistMode = resistMode.STRONG;
            if (m_enemy.resistanceRate > 1f)
                m_resistMode = resistMode.WEAK;
            if (m_enemy.resistanceRate == 1f)
                m_resistMode = resistMode.NORMAL;
        }
        base.normalReaction(value);
    }

    public override void ReactToSpeedUp(float value)
    {
        if (m_enemy) m_enemy.resistanceRate = resistanceWhenSpeed;
        base.ReactToSpeedUp(value);
    }
    public override void ReactToSlowDown(float value)
    {
        if (m_enemy) m_enemy.resistanceRate = resistanceWhenSlow;
        base.ReactToSlowDown(value);
    }
    public override void ReactNormalState()
    {
        if (m_enemy) m_enemy.resistanceRate = resistanceWhenNormal;
        base.ReactNormalState();
    }

    public override void _Update()
    {
        if (m_enemy && m_resistMode != resistMode.NORMAL)
        {
            Color c = Color.white;
            if (m_resistMode == resistMode.STRONG)
                c = strongColor;
            if (m_resistMode == resistMode.WEAK)
                c = weakColor;

            for (int i = 0; i < sprites.Length; ++i)
            {
                sprites[i].color = Color.Lerp(baseColors[i], c, Mathf.PingPong(Time.time, 1f));
            }
        }
        else
            for (int i = 0; i < sprites.Length; ++i)
            {
                sprites[i].color = baseColors[i];
            }
    }
}