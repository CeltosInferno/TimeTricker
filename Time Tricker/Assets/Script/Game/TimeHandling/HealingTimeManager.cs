using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * When the enemy is affected by the time, it heals itself
 */
public class HealingTimeManager : BasicTimeManager
{
    //healing rate per second
    public float healingRate = 1f;
    public Color healColor = Color.green;
    private Color[] baseColors;

    private Enemy m_enemy;
    public SpriteRenderer[] sprites;

    bool isHealing = false;

    // Start is called before the first frame update
    protected override
        void Start()
    {
        base.Start();
        m_enemy = GetComponent<Enemy>();
        baseColors = new Color[sprites.Length];
        for(int i = 0; i < sprites.Length; ++i)
            baseColors[i] = sprites[i].color;
    }

    protected override void normalReaction(float value)
    {
        base.normalReaction(value);
        isHealing = (value != 1f);
    }

    // Update is called once per frame
    public override void _Update()
    {
        if (m_enemy && isHealing)
        {
            m_enemy.TakeDommage(-Time.deltaTime * healingRate);
            for (int i = 0; i < sprites.Length; ++i)
            {
                sprites[i].color = Color.Lerp(baseColors[i], healColor, Mathf.PingPong(Time.time, 1f));
            }
        }
        else
            for (int i = 0; i < sprites.Length; ++i)
            {
                sprites[i].color = baseColors[i];
            }
    }
}
