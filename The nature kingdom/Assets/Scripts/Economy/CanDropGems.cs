using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDropGems : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;

    [SerializeField]
    TempPlayerEconomy tempPlayerEconomy;

    [Header("Pick enemy type")]
    [SerializeField]
    EnemyType typeOfEnemy;
    enum EnemyType
    {
        Enemy1,
        Enemy2,
        Enemy3
    }
    int GemDropAmount;
    private void OnDestroy()//kald denne metode når denne enemy dør
    {        
        if (typeOfEnemy == EnemyType.Enemy1)
        {
            GemDropAmount = tempPlayerEconomy.Enemy1;
        }
        else if (typeOfEnemy == EnemyType.Enemy2)
        {
            GemDropAmount = tempPlayerEconomy.Enemy2;
        }
        else if (typeOfEnemy == EnemyType.Enemy3)
        {
            GemDropAmount = tempPlayerEconomy.Enemy3;
        }

        //GIV SPILLER GemDropAmount ANTAL GEMS HER
        tempPlayerEconomy.Gems += GemDropAmount;
    }
}
