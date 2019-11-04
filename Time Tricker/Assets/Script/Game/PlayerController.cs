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

    Animator anim;

    public GameObject sprites;

    public AudioSource JumpSound;
    public AudioSource MoveSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = sprites.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Move(moveInput);
        Jump(jumpInput);
    }


    void Move(string moveInput)
    {
        Vector3 movement = new Vector3(Input.GetAxis(moveInput), 0f, 0f);
        transform.Translate(movement * Time.deltaTime * speed);

        CharacterFlip(movement);

        if (Input.GetAxis(moveInput) > minToMove || Input.GetAxis(moveInput) < -minToMove)
        {
            anim.SetBool(Variables.movingKey, true);
            if(!MoveSound.isPlaying && rb.velocity == Vector2.zero)
                MoveSound.Play();
        }
        else
        {
            anim.SetBool(Variables.movingKey, false);
            if (MoveSound.isPlaying || rb.velocity != Vector2.zero)
                MoveSound.Stop();
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
            JumpSound.Play();
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
