using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * When an enemy is affected by time, it will mmove, otherwise
 * it will be insensible
 */
public class StoneTimeManager : BasicTimeManager
{

    public Color statueColor = Color.grey;
    bool isStatue = false;

    private Enemy m_enemy;
    private Color[] baseColors;
    public SpriteRenderer[] sprites;

    // Start is called before the first frame update
    protected override
        void Start()
    {
        base.Start();
        m_enemy = GetComponent<Enemy>();
        basicNormalTime = 0f;
        baseColors = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; ++i)
            baseColors[i] = sprites[i].color;
    }

    protected override void normalReaction(float value)
    {
        isStatue = (value == 0f);
        if (isStatue)
        {
            m_enemy.resistanceRate = 0f;
            for (int i = 0; i < sprites.Length; ++i)
            {
                sprites[i].color = statueColor;
            }
        }
        else {
            m_enemy.resistanceRate = 1f;
            for (int i = 0; i < sprites.Length; ++i)
            {
                sprites[i].color = baseColors[i];
            }
        }
        base.normalReaction(value);
    }

    public override void ReactNormalState()
    {
        base.ReactNormalState();
    }
}
