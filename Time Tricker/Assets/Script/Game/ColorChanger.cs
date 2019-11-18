using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Allows to gradually change the color of its SpriteRenderer given a time
 */
public class ColorChanger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    float m_duration = 1f;
    float startTime;

    Color startColor;
    Color aimColor;

    private void Start()
    {
        startTime = Time.time;
        startColor = spriteRenderer.color;
        aimColor = startColor;
    }

    public void changeColor(Color newColor, float duration)
    {
        aimColor = newColor;
        startColor = spriteRenderer.color;
        m_duration = duration;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float current_time = Time.time;
        float intensity = Mathf.Min(1f, (current_time - startTime) / m_duration);
        spriteRenderer.color = Color.Lerp(startColor, aimColor, intensity);
    }
}
