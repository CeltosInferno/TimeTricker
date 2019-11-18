using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float delayDommageEffect;
    public GameObject deathEffect;
    public HealthBar healthBar;

    public float localScaleX = 0.005f;
    public float localScaleY = 0.005f;
    public float delayShowHealthBar = 2f;

    public float dommage;

    public GameObject Hud;

    private float maxHealth;
    public float forceImpactX;
    public float forceImpactY;


    private void Start()
    {
        maxHealth = health;
        healthBar.GetComponent<Transform>().localScale = new Vector3(0.0f, 0.0f);   
    }
    public void Update()
    {
        StartCoroutine(GetComponent<SoundManagerMonster>().Scream());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            GameObject.FindGameObjectsWithTag("Hud")[0].GetComponent<PlayerHealth>().TakeDommage(dommage);
        }
    }

    public void TakeDommage(int dommage)
    {
        Debug.Log("Enemy hit" + dommage);

        GetComponent<Animator>().SetTrigger("isTakingDommage");

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

    IEnumerator ShowHealthBar()
    {
        healthBar.GetComponent<Transform>().localScale = new Vector3(localScaleX, localScaleY);
        yield return new WaitForSeconds(delayShowHealthBar);
        healthBar.GetComponent<Transform>().localScale = new Vector3(0f, 0f);
    }
}
