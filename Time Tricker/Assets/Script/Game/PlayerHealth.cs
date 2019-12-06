using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Health bar
    public HealthBar healthBar;
    //Value of health
    public float health;

    //Armor bar
    public HealthBar armorBar;
    //Value of armor
    public float armor;

    //Percentage of dommage wich penetrate armor
    [Range(0.0f, 1.0f)]
    public float percentageArmorPenetration;

    private GameObject player;

    //Time in second of invincibility
    public float invincibilityTime;
    //If the player is dead
    private bool gameOver;

    private Animator playerAnimator;

    //Max value of health
    private float maxHealth;
    //Max value of armor
    private float maxArmor;
    //If the player is invincible
    private bool invincibility;

    void Start()
    {
        maxHealth = health;
        maxArmor = armor;
        gameOver = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().gameHasEnded;
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnimator = player.transform.Find("Sprites").GetComponent<Animator>();
    }

    void Update()
    {
        gameOver = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>().gameHasEnded;
    }

    /**
     * Give dommage to the right bar. Manage health and armor bar
     * <param name="dommage">Amount of dommage taken by player</param>
     * <returns>Void</returns>
     **/
    public void TakeDommage(float dommage)
    {
        //Take dommage of no invisibility
        if(!invincibility)
        {
            Debug.Log("Player hit" + dommage);
            player.GetComponent<SoundManagerProfessorX>().PlaySoundHurt();
            invincibility = true;

            if (armor > 0)
            {
                UpdateArmor(dommage);
                //Update health bar with dommage penetrate armor
                UpdateHealth(dommage * percentageArmorPenetration);
            }
            else
            {
                UpdateHealth(dommage);
            }

            if (health <= 0)
            {
                Die();
            }
            else
            {
                //Reset invisibility after an amount of time
                Invoke("ResetInvincibility", invincibilityTime);
            }
        }
    }

    /**
     * Manage death player
     * <returns>Void</returns>
     **/
    public void Die()
    {
        invincibility = true;
        Debug.Log("Player is dead");
        
        GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>().gameHasEnded = true;
        playerAnimator.SetBool("isDead", true);
        player.GetComponent<SoundManagerProfessorX>().PlaySoundDead();
    }

    /**
     * Reset the value of invincibility
     * <returns>Void</returns>
     **/
    void ResetInvincibility()
    {
        if(!playerAnimator.GetBool("isDead"))
        {
            invincibility = false;
        }
    }

    /**
     * Update the value of health and the health bar
     * <param name="dommage">Dommage taken</param>
     * <returns>Void</returns>
     **/
    void UpdateHealth(float dommage)
    {
        health -= dommage;
        if (health < 0)
        {
            health = 0;
        }
        //Update health bar
        healthBar.SetSize(health / maxHealth);
        playerAnimator.SetTrigger("isTakingDamage");
    }

    /**
     * Update the value of armor and the amor bar
     * <param name="dommage">Dommage taken</param>
     * <returns>Void</returns>
     **/
    void UpdateArmor(float dommage)
    {
        armor -= dommage;
        if (armor < 0)
        {
            armor = 0;
        }
        //Update armor bar
        armorBar.SetSize(armor / maxArmor);
        playerAnimator.SetTrigger("isTakingDamage");
    }
}
