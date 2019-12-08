using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Represent an enemy that shoots at distance
 */
public class IAEnemyShooter : IAEnemy
{
    public enum shooterState
    {
        SHOOTING,
        TOO_FAR,
        TOO_CLOSE,
        RELOADING
    };

    [System.Serializable]
    public class Ammunition
    {
        public GameObject bulletType;
        public int quantity;
        public float shootRate;
    }

    public Ammunition[] ammunitions;

    public float reloadSpeed = 1f;
    public float minDistance = 10f;
    public float maxDistance = 30f;

    public GameObject flashFire;
    public GameObject flashFire2;
    public GameObject flashFire3;
    public Transform shotPoint;
    public AudioClip fireClip;

    protected float currentDistance;

    private int currentAmmoId = 0;
    private int currentAmmoQuantity;
    private Ammunition currentAmmo;
    private float timeOfAction;
    private shooterState state = shooterState.RELOADING;

    public override void Start()
    {
        base.Start();
        LoadWeapon(currentAmmoId);
        timeOfAction = 0f;
    }

    public void ChangeState(shooterState s)
    {
        if (state != s)
        {
            state = s;
            timeOfAction = 0f;
        }
    }

    protected override void Move()
    {
        directionMove = positionPlayer.position.x - transform.position.x;
        if (tooClose())
            directionMove *= -1; //we flee instead of following
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

    public override void Update()
    {
        //decision
        currentDistance = (positionPlayer.position - transform.position).magnitude;
        if (tooClose()) 
            ChangeState(shooterState.TOO_CLOSE);
        else if (tooFar())
            ChangeState(shooterState.TOO_FAR);
        else if(needReload())
            ChangeState(shooterState.RELOADING);
        else
            ChangeState(shooterState.SHOOTING);
        timeOfAction += Time.deltaTime * m_timeScale;

        if(goodPosition())
            animator.SetBool("isMoving", false);
        //action
        switch (state)
        {
            case shooterState.RELOADING:
                if (canReload())
                    Reload();
                break;
            case shooterState.SHOOTING:
                if (!EnoughAmmo() && !needReload())
                    LoadWeapon(currentAmmoId + 1);
                if (canShoot())
                    Shoot();
                break;
            case shooterState.TOO_CLOSE:
            case shooterState.TOO_FAR:
                base.Update();
                break;
        }
    }

    void LoadWeapon(int id)
    {
        currentAmmoId = id;
        currentAmmo = ammunitions[id];
        currentAmmoQuantity = currentAmmo.quantity;
        timeOfAction = 0f;
    }

    bool tooClose()
    {
        return (currentDistance < minDistance);
    }

    bool tooFar()
    {
        return (currentDistance > maxDistance);
    }

    bool EnoughAmmo()
    {
        return currentAmmoQuantity > 0;
    }

    bool goodPosition() {
        return (!tooClose()
            && !tooFar());
    }

    bool canShoot() {
        return (EnoughAmmo()
            && timeOfAction >= currentAmmo.shootRate);
    }

    bool needReload()
    {
        return !EnoughAmmo() 
            && currentAmmoId >= ammunitions.Length -1;
    }

    bool canReload()
    {
        return timeOfAction >= reloadSpeed;
    }

    void Reload()
    {
        LoadWeapon(0);
        state = shooterState.SHOOTING;
        timeOfAction = 0f;
    }

    void Shoot()
    {
        Vector3 difference = positionPlayer.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, rotZ);
        Instantiate(currentAmmo.bulletType, 
            shotPoint.position,
            rotation);
        currentAmmoQuantity -= 1;
        animator.SetTrigger("isFiring");
        switch (Random.Range(0, 3))
        {
            case 0:
                Instantiate(flashFire, shotPoint.position, rotation);
                break;
            case 1:
                Instantiate(flashFire2, shotPoint.position, rotation);
                break;
            case 2:
                Instantiate(flashFire3, shotPoint.position, rotation);
                break;
            default:
                Debug.LogError("Fire - Error animation muzzle flash");
                break;
        }

        GetComponent<SoundManager>().PlaySound(fireClip);
        timeOfAction = 0f;
    }
}
