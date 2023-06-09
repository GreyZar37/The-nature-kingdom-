using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ally : MonoBehaviour
{
    public int direction
    {
        get
        {
            return (int)Mathf.Sign(currentFollowObj.transform.position.x - transform.position.x);
        }
    }

    [TextArea]
    public string note;

    [Header("Properties")]
    public float speed;
    public int damage;
    public int Health;
    public float attackDistance;
    public int sight;
    public float attakcCooldown;
    public float blockCooldown;
    public float blockTime;
    public bool block;
    public bool isDashing;

    public bool stuned;
    public float stunedTimer;


    [Header("Components")]
    public Transform currentFollowObj;
    public Transform guardingSpot;



    public GameObject Enemy;
    public GameObject[] GuardSpots;


    public Rigidbody2D rb;
    public Animator anim;
    public Transform attackPos;
    [Header("Other")]
    public float DistanceToTarget;
    public LayerMask EnemyMask;

    public AudioClip[] hurtSounds;
    public AudioClip[] attackSounds;

    private void Awake()
    {
    }
    public virtual void Start()
    {
        GuardSpots = GameObject.FindGameObjectsWithTag("GuardPoint");
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        if (GuardSpots[0].transform.GetComponent<GuardSpot>().guards < GuardSpots[1].transform.GetComponent<GuardSpot>().guards)
        {
            guardingSpot = GuardSpots[0].transform;
            GuardSpots[0].transform.GetComponent<GuardSpot>().addGuard();
            currentFollowObj = guardingSpot;

        }
        else
        {
            guardingSpot = GuardSpots[1].transform;
            GuardSpots[1].transform.GetComponent<GuardSpot>().addGuard();
            currentFollowObj = guardingSpot;
        }

    }

    public virtual void Update()
    {
        if(currentFollowObj == null)
        {
            currentFollowObj = guardingSpot;
        }   
        DistanceToTarget = Vector2.Distance(transform.position, currentFollowObj.transform.position);
        
    }



    public virtual void takeDamage(int damage)
    {
        AudioManager.PlaySound(hurtSounds, 1);

        Health -= damage;
        if (Health <= 0)
        {
            die();
        }
        anim.SetTrigger("Hit");
    }
    public void die()
    {

        anim.SetTrigger("Die");
        Destroy(this);

        gameObject.AddComponent<DestroyObject>();
        Destroy(gameObject.GetComponent<Collider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());

    }

   


}
