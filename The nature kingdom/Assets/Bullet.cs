using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb2D;
    [SerializeField] float speed;
    [SerializeField] int damage;



    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(destoryAfterTime());
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.velocity = -transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyMeleManager>().takeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.tag == "Ground")
        {
            Destroy(gameObject);
        }

    }

    IEnumerator destoryAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
