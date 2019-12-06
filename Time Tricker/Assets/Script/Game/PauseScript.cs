using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private bool isPaused;
    private float previousScale;
    [SerializeField]
    private GameObject[] toDeactivate;
    [SerializeField]
    private GameObject[] toActivate;

    void Start()
    {
        isPaused = false;
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
                foreach(GameObject obj in toDeactivate){
                    obj.SetActive(false);
                }
                foreach (GameObject obj in toActivate)
                {
                    obj.SetActive(true);
                }
            }
            else
            {
                Time.timeScale = previousScale;
                foreach (GameObject obj in toDeactivate)
                {
                    obj.SetActive(true);
                }

                foreach (GameObject obj in toActivate)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
