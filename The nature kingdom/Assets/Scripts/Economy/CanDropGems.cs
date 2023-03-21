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

    //Burde nok ligge i et andet script, for at undgå at de her variabler skal sættes for hver individuel enemy
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
        //kald denne metode når denne enemy dør

        //GIV SPILLER GemDropAmount ANTAL GEMS HER
        Debug.Log($"Spiller skal have +{GemDropAmount} antal gems");
        //alternativt: Spawn en gem sprite med en Button som spilleren skal klikke på for at samle gems op
    }
}
