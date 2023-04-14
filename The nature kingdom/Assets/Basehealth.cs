using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Basehealth : MonoBehaviour
{
    public int maxHealth;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public  void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
