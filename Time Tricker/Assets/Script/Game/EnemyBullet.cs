using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Represent a bullet emited from an anemy whose goal is to 
 * touch the player
 * (note : player and enemy should have the same base script to handle damage)
 */
public class EnemyBullet : Bullet
{
    protected override
        void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        //Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            GameObject.FindGameObjectsWithTag("Hud")[0].GetComponent<PlayerHealth>().TakeDommage(bulletDamage);
        }

        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        //Instantiate(particuleImpact, transform.position, Quaternion.identity);
        //particuleImpact.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
