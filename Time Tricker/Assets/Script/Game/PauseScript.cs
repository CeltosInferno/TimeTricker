using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private bool isPaused;
    private float previousScale;
    private SpriteRenderer Sprite;
    [SerializeField]
    private GameObject bars;

    void Start()
    {
        isPaused = false;
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                previousScale = Time.timeScale;
                Time.timeScale = 0;
                Sprite.enabled = true;
                bars.SetActive(true);
            }
            else
            {
                Time.timeScale = previousScale;
                Sprite.enabled = false;
                bars.SetActive(false);
            }
        }
    }
}
