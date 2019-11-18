using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBtwShot;

    public GameObject flashFire;
    public GameObject flashFire2;
    public GameObject flashFire3;

    private float timestamp = 0.0f;

    // Update is called once per frame
    void Update()
    {
        bool gameHasEnded = GameObject.FindGameObjectsWithTag("GameManager")[0].GetComponent<GameManager>().gameHasEnded;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (Input.GetMouseButton(0) && Time.time > timestamp && !gameHasEnded)
        {
            timestamp = Time.time + timeBtwShot;
            Instantiate(projectile, shotPoint.position, transform.rotation);
            switch (Random.Range(0, 3))
            {
                case 0:
                    Instantiate(flashFire, shotPoint.position, transform.rotation);
                    Debug.Log("Fire - Muzzle flash anim 1");
                    break;
                case 1:
                    Instantiate(flashFire2, shotPoint.position, transform.rotation);
                    Debug.Log("Fire - Muzzle flash anim 2");
                    break;
                case 2:
                    Instantiate(flashFire3, shotPoint.position, transform.rotation);
                    Debug.Log("Fire - Muzzle flash anim 3");
                    break;
                default:
                    Debug.LogError("Fire - Error animation muzzle flash");
                    break;
            }

            GetComponent<SoundManagerGun>().PlaySound();
        }
    }
}
