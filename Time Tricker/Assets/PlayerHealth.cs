using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public float health;
    public float invincibilityTime;
    public bool gameOver;

    private float maxHealth;
    private bool invincibility;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        gameOver = false;
    }

    public void TakeDommage(int dommage)
    {
        if(!invincibility)
        {
            invincibility = true;
            Debug.Log("Player hit" + dommage);

            health -= dommage;
            if(health < 0)
            {
                health = 0;
            }

            healthBar.SetSize(health / maxHealth);

            Invoke("ResetInvincibility", invincibilityTime);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Debug.Log("Player is dead");
        gameOver = true;
    }

    void ResetInvincibility()
    {
        invincibility = false;
    }
}
