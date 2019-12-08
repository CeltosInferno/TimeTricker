using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Represent a generic enemy with statistics and health
 */
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

    //multiply the damage taken by the enemy
    public float resistanceRate = 1f;

    protected SoundManagerMonster soundManager;

    public float getMaxHealth() {
        return maxHealth;
    }

    private void Start()
    {
        maxHealth = health;
        healthBar.GetComponent<Transform>().localScale = new Vector3(0.0f, 0.0f);
        soundManager = GetComponent<SoundManagerMonster>();
    }
    public void Update()
    {
        StartCoroutine(soundManager.Scream());
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            GameObject.FindGameObjectsWithTag("Hud")[0].GetComponent<PlayerHealth>().TakeDommage(dommage);
        }
    }

    public void TakeDommage(float damageTaken)
    {
        float damage = damageTaken;
        if (damageTaken > 0) {
            damage *= resistanceRate;
        }
        Debug.Log("Enemy hit" + damage);

        if (damage != 0f)
        {
            if (damage > 0f)
            {
                if (Random.Range(0, 3) == 0) 
                    soundManager.playSoundHurt();
                GetComponent<Animator>().SetTrigger("isTakingDamage");
            }
            health = Mathf.Min(health - damage, maxHealth);
            healthBar.SetSize(health / maxHealth);

            StartCoroutine(ShowHealthBar());
        }
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
