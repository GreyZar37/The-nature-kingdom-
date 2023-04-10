using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;
    bool attacking;
    [SerializeField]
    int damage;
    [SerializeField]
    float attackRange;
    [SerializeField]
    int MaxHealth;
    public int currentHealth;
    public float attackSpeed;
    public LayerMask enemies;
    public AudioClip[] attackSound;
    public AudioClip[] hurtSound;

    bool stuned;
    [SerializeField]

    float stunnedTimer;

    [SerializeField]
    Transform attackPos;

    Coroutine attackRoutine = null;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attacking && !stuned) 
        {
            attackRoutine = StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        AudioManager.PlaySound(attackSound, 1);

        attacking = true;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(attackSpeed);
        anim.SetBool("Attacking", false);
        attacking = false;

        Collider2D hit = Physics2D.OverlapCircle(attackPos.position, attackRange, enemies);
        if (hit != null && !stuned)
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.GetComponent<EnemyMeleManager>().takeDamage(damage);
            }

        }
    }

    public void TakeDamage(int damage)
    {
        AudioManager.PlaySound(hurtSound, 1);

        attacking = false;
        anim.SetBool("Attacking", false);

        anim.SetTrigger("Hit");
        currentHealth -= damage;
        if(attackRoutine != null)
        StopCoroutine(attackRoutine);
        StartCoroutine(stun());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);


    }

    IEnumerator stun()
    {
        stuned = true;
       
        yield return new WaitForSeconds(stunnedTimer);

        stuned = false;

    }
}
