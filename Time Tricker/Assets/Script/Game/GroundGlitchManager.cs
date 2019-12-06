using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet de vérifier et tuer les élément glitchés et les détruire

public class GroundGlitchManager : MonoBehaviour
{
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COUCOU");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
    */
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit Game Area Detected");
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Out of Game Area");
            PlayerHealth PV = GameObject.FindObjectOfType<PlayerHealth>();
            Debug.Log(PV);
            if (PV != null)
            {
                Debug.Log("Player killed?");
                PV.Die();
            }
        }
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Out of Game Area");
            Enemy E = collision.gameObject.GetComponent<Enemy>();
            if (E != null)
            {
                Debug.Log("Enemy killed");
                E.TakeDommage(int.MaxValue);
            }
        }
    }
}
