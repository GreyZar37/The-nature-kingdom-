using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public int damage;

    public int Health;

    public GameObject PlayerBase;
    public GameObject Player;

    public float DistanceToPlayer;

    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerBase = GameObject.FindGameObjectWithTag("PlayerBase");
    }

    public virtual void Update()
    {
        DistanceToPlayer = Vector2.Distance(transform.position, Player.transform.position);
    }

    public abstract void attack(int damage);
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

}
