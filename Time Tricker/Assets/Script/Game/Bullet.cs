using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float forceImpactX;
    public float forceImpactY;

    public float limitationApplicationForce;

    public GameObject destroyEffect;
    public GameObject particuleImpact;
    public GameObject referentiel;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", (float)lifetime * Time.deltaTime / Time.unscaledDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.unscaledDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            GameObject enemyGameObject = collision.gameObject;
            enemy.TakeDommage(20);

            if (LimitApplicationForce(enemyGameObject))
            {
                enemyGameObject.GetComponent<Rigidbody2D>().AddForce(referentiel.GetComponent<Transform>().transform.up * forceImpactY, ForceMode2D.Impulse);
                enemyGameObject.GetComponent<Rigidbody2D>().AddForce(GetComponent<Transform>().transform.right * forceImpactX, ForceMode2D.Impulse);
            } 
        }

        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Instantiate(particuleImpact, transform.position, Quaternion.identity);
        particuleImpact.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }

    void DestroyProjectile()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    bool LimitApplicationForce(GameObject gameObject)
    {
        return gameObject.GetComponent<Rigidbody2D>().velocity.y < limitationApplicationForce &&
                gameObject.GetComponent<Rigidbody2D>().velocity.y > -limitationApplicationForce &&
                gameObject.GetComponent<Rigidbody2D>().velocity.x < limitationApplicationForce &&
                gameObject.GetComponent<Rigidbody2D>().velocity.x > -limitationApplicationForce;
    }
}
