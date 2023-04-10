using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
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
    public GameObject currentFollowObj;
    public GameObject PlayerBase;
    public GameObject Player;
    public Rigidbody2D rb;
    public Animator anim;
    public Transform attackPos;
    [Header("Other")]
    public float DistanceToTarget;
    public LayerMask mobs;

    public AudioClip[] hurtSounds;
    public AudioClip[] hurtSoundsStab;

    public AudioClip[] attackSounds;


    private void Awake()
    {
    }
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerBase = GameObject.FindGameObjectWithTag("PlayerBase");
        rb = GetComponent<Rigidbody2D>();
        currentFollowObj = PlayerBase;

    }

    public virtual void Update()
    {
        if (currentFollowObj == null)
        {
            currentFollowObj = PlayerBase;
        }
        DistanceToTarget = Vector2.Distance(transform.position, currentFollowObj.transform.position);
    }



    public virtual void takeDamage(int damage)
    {
        AudioManager.PlaySound(hurtSounds, 1);
        AudioManager.PlaySound(hurtSoundsStab, 1);

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
