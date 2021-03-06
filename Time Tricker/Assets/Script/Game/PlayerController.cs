﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //vitesse du joueur
    public float speed = 3f;
    public float jumpForce = 5f;
    public bool isGrounded = false;
    public float minToMove = 0.1f;

    private Rigidbody2D rb;
    public ParticleSystem movingDust; 

    private string moveInput = "Horizontal";
    private string jumpInput = "Jump";
    private bool isFacingRight = true;
    private SoundManagerProfessorX SoundManager;


    Animator anim;

    public GameObject sprites;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = sprites.GetComponent<Animator>();
        SoundManager = GetComponent<SoundManagerProfessorX>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("isDead"))
        {
            Move(moveInput);
            Jump(jumpInput);

        }
        else
        {
            //reset isMoving to play death animation
            anim.SetBool("isMoving", false);
        }
    }

    /* Applique toutes les actions nécessaires lors d'un mouvement simple (gauche ou droite) */
    void Move(string moveInput)
    {
        //On crée l'action de mouvement
        Vector3 movement = new Vector3(Input.GetAxis(moveInput), 0f, 0f);
        transform.Translate(movement * Time.deltaTime * speed);

        //Si nécessaire, on fait changer le personnage de direction
        CharacterFlip(movement);


        //On joue le son de mouvement, et on le coupe si on arrête de bouger
        if (Input.GetAxis(moveInput) > minToMove || Input.GetAxis(moveInput) < -minToMove)
        {
            anim.SetBool(Variables.movingKey, true);
            if (!SoundManager.MoveAudioSound.isPlaying && rb.velocity == Vector2.zero)
            {
                SoundManager.PlaySoundMove();
            }
        }
        else
        {
            anim.SetBool(Variables.movingKey, false);
            if (SoundManager.MoveAudioSound.isPlaying || rb.velocity != Vector2.zero)
            {
                SoundManager.StopSoundMove();
            }
        }

        //On active notre poussière
        CreateMovingDust(movement);

    }

    //Permet de détecter si on a changé de direction, et de changer d'orientation si besoin
    private void CharacterFlip(Vector3 movement)
    {
        if ((movement.x < 0 && isFacingRight) || (movement.x > 0 && !isFacingRight))
        {
            Flip();
        }
    }

    void Jump(string jumpInput)
    {
        if (Input.GetButtonDown(jumpInput) && isGrounded == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce) , ForceMode2D.Impulse);
            anim.SetTrigger(Variables.jumpingKey);

            SoundManager.PlaySoundJump();
        }
    }

    /* Permet au sprite du personnage de changer d'orientation lorsqu'il change de direction*/
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 characterScale = sprites.transform.localScale;
        characterScale.x *= -1;
        sprites.transform.localScale = characterScale;
    }

    //Gère le particleSystem simulant la poussière du mouvement (on checke si on touche un sol pour activer l'effet)
    void CreateMovingDust(Vector3 movement)
    {
        if (isGrounded && movement.x != 0)
        {
            movingDust.Play();

        }
        else
        {
            movingDust.Stop();
        }
    }
}
