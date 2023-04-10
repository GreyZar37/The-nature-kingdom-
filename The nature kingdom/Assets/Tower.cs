using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum states
{
    idle, combat
}



public class Tower : MonoBehaviour
{

    [TextArea]
    public string note;

    [Header("Properties")]

    [SerializeField] float sightRange;
    
    public float RotationSpeed;
    public float roateModif;

    public float shootCooldown;
    bool onCooldown;

    private Vector2 _direction;


    [Header("Components")]
    [SerializeField] EnemyMeleManager fokusedEnemy;
    [SerializeField] GameObject towerHead;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPos;


    [Header("Other")]
    public states currentState;
    [SerializeField] LayerMask enemyMask;


    [SerializeField] AudioClip[] shootSound;

    private void Start()
    {
        currentState = states.idle;
    }

    private void Update()
    {
        if (currentState == states.idle)
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, sightRange, enemyMask);
            foreach (var enemy in hit)
            {
                fokusedEnemy = enemy.GetComponent<EnemyMeleManager>();
                currentState = states.combat;
            }
        }
        else
        {
        
            if(onCooldown == false)
            {
                StartCoroutine(shoot());
            }


            if (fokusedEnemy != null)
            {
                if (Vector2.Distance(transform.position, fokusedEnemy.gameObject.transform.position) > sightRange)
                {
                    fokusedEnemy = null;
                    currentState = states.idle;

                }
                else
                {
                    _direction = towerHead.transform.position - fokusedEnemy.transform.position;
                    float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg - roateModif;
                    Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
                    towerHead.transform.rotation = Quaternion.RotateTowards(towerHead.transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
                }

             
            }
            else
            {
                currentState = states.idle;
            }
        }

       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    IEnumerator shoot()
    {
        AudioManager.PlaySound(shootSound, 1f);
        onCooldown = true;
        Instantiate(bullet, shootingPos.position, towerHead.transform.rotation);
        yield return new WaitForSeconds(shootCooldown);
        onCooldown = false;

    }

}


