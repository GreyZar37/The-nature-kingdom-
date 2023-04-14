using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDropGems : MonoBehaviour
{
    [TextArea][SerializeField] string notes;
    TempPlayerEconomy tempPlayerEconomy;
    public int GemDropAmount;
    private void Awake()
    {
        tempPlayerEconomy = FindObjectOfType<TempPlayerEconomy>();
    }
    private void OnDestroy()//kald denne metode n�r denne enemy d�r
    {
        //Giver spiller gems
        tempPlayerEconomy.Gems += GemDropAmount;
    }
}
