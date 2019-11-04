using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroy : MonoBehaviour
{
    public float delay = 0f;
    public GameObject sprite;

    // Use this for initialization
    void Start()
    {
        if (sprite.activeSelf)
        {
            float delayDestroy = sprite.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay;
            Destroy(gameObject, delayDestroy);
        }
    }
}
