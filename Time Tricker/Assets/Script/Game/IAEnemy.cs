using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Represent the behaviour of a basic enemy :
 * it follows the player and jumps
 */
public class IAEnemy : TimeEntity
{
    public float speed;
    public float forceJump;
    public float delayJump;
    public float delayStarMoving;
    public float forceMove;

    private Transform positionPlayer;
    private float directionMove;
    private bool canJump;
    private bool isFacingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        positionPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DelayMoving());
        if (GetComponent<Animator>().GetBool("isMoving"))
        {
            Move();
        }
    }

    private void Move()
    {
        directionMove = positionPlayer.position.x - GetComponent<Transform>().position.x;
        if (directionMove < 0)
        {
            if (isFacingRight) Flip();
            //transform.Translate(Vector3.left * speed * m_timeScale * Time.deltaTime);
            TimeTranslate(transform, Vector3.left * speed * Time.deltaTime);
            //rb.AddForce(-transform.right * forceMove, ForceMode2D.Force);
        }
        else
        {
            if (!isFacingRight) Flip();
            //transform.Translate(Vector3.right * speed * m_timeScale * Time.deltaTime);
            TimeTranslate(transform, Vector3.right * speed * Time.deltaTime);
            //rb.AddForce(transform.right * forceMove, ForceMode2D.Force);
        }
    }

    //On déclenche potentiellement un saut à chaque rencontre de bumper si le joueur est en hauteur
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canJump && positionPlayer.position.y > rb.position.y) 
        {
            canJump = false;
            StartCoroutine(DelayJump());
            Debug.Log("Jump because collision with" + collision.gameObject.name);
            TimeAddForce(rb, transform.up * forceJump, ForceMode2D.Impulse);
            //rb.AddForce(transform.up * forceJump * Mathf.Sqrt(m_timeScale), ForceMode2D.Impulse);
        }
    }

    private IEnumerator DelayMoving()
    {
        yield return new WaitForSeconds(delayJump);
        GetComponent<Animator>().SetBool("isMoving", true);
    }

    private IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(delayStarMoving);
        canJump = true;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
