using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDropGems : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;


    [Header("Pick enemy type")]
    [SerializeField]
    EnemyType typeOfEnemy;
    enum EnemyType
    {
        Enemy1,
        Enemy2,
        Enemy3
    }

    //Burde nok ligge i et andet script, for at undg� at de her variabler skal s�ttes for hver individuel enemy
    [Header("GemDropAmount")]
    public int GemDropAmount;
    [SerializeField]
    int Enemy1, Enemy2, Enemy3;

    // Start is called before the first frame update
    void Start()
    {
        if (typeOfEnemy == EnemyType.Enemy1)
        {
            GemDropAmount = Enemy1;
        }
        else if (typeOfEnemy == EnemyType.Enemy2)
        {
            GemDropAmount = Enemy2;
        }
        else if (typeOfEnemy == EnemyType.Enemy3)
        {
            GemDropAmount = Enemy3;
        }
    }

    public void AddGems()
    {
        //kald denne metode n�r denne enemy d�r

        //GIV SPILLER GemDropAmount ANTAL GEMS HER
        Debug.Log($"Spiller skal have +{GemDropAmount} antal gems");
        //alternativt: Spawn en gem sprite med en Button som spilleren skal klikke p� for at samle gems op
    }
}
