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

    protected Transform positionPlayer;
    protected float directionMove;
    private bool canJump;
    private bool isFacingRight = true;
    private Rigidbody2D rb;
    protected Animator animator;
    //private Enemy enemyStats;

    public virtual
    void Start()
    {
        positionPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //enemyStats = GetComponent<Enemy>();
        canJump = true;
    }

    // Update is called once per frame
    public virtual
        void Update()
    {
        StartCoroutine(DelayMoving());
        if (animator.GetBool("isMoving"))
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        directionMove = positionPlayer.position.x - GetComponent<Transform>().position.x;
        //float minDist = 0.05f;
        if (directionMove < 0)
        {
            MoveDirection(true);
        }
        else if (directionMove > 0)
        {
            MoveDirection(false);
        }
    }

    //move left
    //else, move right
    protected void MoveDirection(bool left)
    {
        if(left)
            TimeTranslate(transform, Vector3.left * speed * Time.deltaTime);
        else
            TimeTranslate(transform, Vector3.right * speed * Time.deltaTime);
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

    protected IEnumerator DelayMoving()
    {
        yield return new WaitForSeconds(delayJump);
        animator.SetBool("isMoving", true);
    }

    protected IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(delayStarMoving);
        canJump = true;
    }

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        /*Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        //the health bar is inversed in order to keep it in the right way
        //(ugly code but couldn't find another way)
        HealthBar bar = enemyStats.healthBar;
        scale = bar.transform.localScale;
        if (scale.x > 0)
        {
            scale.x *= -1;
            bar.transform.localScale = scale;
        }
        Vector3 pos = bar.transform.position;
        pos.x *= -1;
        bar.transform.position = pos;
        bar.sizeMultiplier *= -1;*/
    }
}
