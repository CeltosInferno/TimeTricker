using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{

    private Rigidbody2D rb = new Rigidbody2D();
    private Animator anim;

    //Vérifie l'état de la chute en cours (pour limiter le nombre d'appels du velocity check)
    private bool checkFallingState = false;
    // Start is called before the first frame update


    public GameObject sprites;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = sprites.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFalling();
    }

    /* Permet de modifier le booléen activant ou non l'animation de tomber*/
    private void CheckFalling()
    {
        if (checkFallingState.Equals(true))
        {
            /* Si la vélocité en y du rigidBody du personnage est négative, cela veut dire qu'il est en train de tomber */
            if (rb.velocity.y < 0)
            {
                anim.SetBool(Variables.fallingKey, true);
                checkFallingState = false;
            }
        }
        if (checkFallingState.Equals(false))
        {
            if (rb.velocity.y >= 0)
            {
                anim.SetBool(Variables.fallingKey, false);
                checkFallingState = true;
            }
        }

    }
}
