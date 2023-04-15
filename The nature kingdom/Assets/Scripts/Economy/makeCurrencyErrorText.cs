using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MakeCurrencyErrorText : MonoBehaviour
{
    [SerializeField] GameObject ObjectTemplate;

    NotEnoughCurrency notEnoughCurrency;

    public Vector3 messagePos;
    public string addonText;
    public void NewMessage()
    {
        //Denne måde at udregne positionen på er ikke baseret på logik, men finjustering af numerne efter hvad resultatet bliver i editoren
        messagePos = new Vector3((messagePos.x + (messagePos.x * 2)) - 50, (messagePos.y - 140), 0);

        notEnoughCurrency.ErrorTextAddon = " " + addonText + "!";

        //instaniate ny error text og fiks positionen
        Instantiate(ObjectTemplate, messagePos, Quaternion.Euler(0, 0, 0), this.gameObject.transform).GetComponent<NotEnoughCurrency>().thisCoolPos = messagePos;

        //Set teksten igang med at animere
        this.transform.GetChild(this.transform.childCount - 1).GetComponent<NotEnoughCurrency>().MakeMessage();
    }
}
