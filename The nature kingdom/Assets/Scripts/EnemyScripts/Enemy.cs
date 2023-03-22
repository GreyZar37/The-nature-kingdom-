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

    [Header("Components")]
    public GameObject currentFollowObj;
    public GameObject PlayerBase;
    public GameObject Player;
    public Rigidbody2D rb;
    public Animator anim;

    [Header("Other")]
    public float DistanceToTarget;
    public LayerMask mobs;
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
        DistanceToTarget= Vector2.Distance(transform.position, currentFollowObj.transform.position);
    }



    public virtual void takeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            die();
        }
    }
    public void die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sight);

        Gizmos.DrawWireSphere(transform.position, sight / 2);

    }


}
