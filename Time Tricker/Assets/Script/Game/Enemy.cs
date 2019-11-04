using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float delayDommageEffect;
    public GameObject deathEffect;
    public HealthBar healthBar;
    public AudioClip audioClipScream1;
    public AudioClip audioClipScream2;

    public float localScaleX = 0.005f;
    public float localScaleY = 0.005f;
    public float delayShowHealthBar = 2f;

    public float soundTimeMin;
    public float soundTimeMax;

    private float maxHealth;
    private bool isScreaming;
    private AudioSource soundScream;
    private float mainVolume;

    private void Start()
    {
        maxHealth = health;
        healthBar.GetComponent<Transform>().localScale = new Vector3(0.0f, 0.0f);
        soundScream = GetComponent<AudioSource>();
        isScreaming = false;
        mainVolume = PlayerPrefs.GetFloat("MainVolume");
    }
    public void Update()
    {
        StartCoroutine(StopAnimDommage());
        StartCoroutine(Scream());
    }

    public void TakeDommage(int dommage)
    {
        Debug.Log("Enemy hit" + dommage);

        GetComponent<Animator>().SetBool("isTakingDommage", true);
        health -= dommage;
        healthBar.SetSize(health / maxHealth);
        StartCoroutine(ShowHealthBar());

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator StopAnimDommage()
    {
        if (GetComponent<Animator>().GetBool("isTakingDommage"))
        {
            yield return new WaitForSeconds(delayDommageEffect);
            GetComponent<Animator>().SetBool("isTakingDommage", false);
        }
    }
    IEnumerator ShowHealthBar()
    {
        healthBar.GetComponent<Transform>().localScale = new Vector3(localScaleX, localScaleY);
        yield return new WaitForSeconds(delayShowHealthBar);
        healthBar.GetComponent<Transform>().localScale = new Vector3(0f, 0f);
    }

    IEnumerator Scream()
    {
        float time = Random.Range(soundTimeMin, soundTimeMax);
        yield return new WaitForSeconds(time);
        time = Random.Range(soundTimeMin, soundTimeMax);
        if (!isScreaming)
        {
            isScreaming = true;
            switch (Random.Range(0, 2))
            {
                case 0:
                    soundScream.PlayOneShot(audioClipScream1, mainVolume);
                    break;
                case 1:
                    soundScream.PlayOneShot(audioClipScream2, mainVolume);
                    break;
                default:
                    Debug.LogError("Monster scream - Error");
                    break;
            }
            yield return new WaitForSeconds(time);
            isScreaming = false;
        }
    }

}
