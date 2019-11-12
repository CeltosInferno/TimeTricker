using System.Collections;
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


    void Move(string moveInput)
    {
        Vector3 movement = new Vector3(Input.GetAxis(moveInput), 0f, 0f);
        transform.Translate(movement * Time.deltaTime * speed);

        CharacterFlip(movement);

        if (Input.GetAxis(moveInput) > minToMove || Input.GetAxis(moveInput) < -minToMove)
        {
            anim.SetBool(Variables.movingKey, true);
            if (!SoundManager.MoveSound.isPlaying && rb.velocity == Vector2.zero)
            {
                SoundManager.PlaySoundMove();
            }
        }
        else
        {
            anim.SetBool(Variables.movingKey, false);
            if (SoundManager.MoveSound.isPlaying || rb.velocity != Vector2.zero)
            {
                SoundManager.StopSoundMove();
            }
        }
    }

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
}
