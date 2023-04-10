using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DustForger : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;

    //hvor mange Gems det koster at forge dust
    public int dustCost;

    //hvor meget dust spilleren får
    public int dustAmount;

    [Header("Components")]
    [SerializeField]
    TempPlayerEconomy tempPlayerEconomy;
    //GO = GameObject
    [SerializeField]
    GameObject CollectDustButton;
    [SerializeField]
    makeCurrencyErrorText makeCurrencyErrorText;

    bool dustAvailable = false; //om der er dust tilgængeligt for spilleren at samle op

    // Start is called before the first frame update
    void Start()
    {
        CollectDustButton.SetActive(false);
    }
    public void ForgeDust()
    {
        if (tempPlayerEconomy.Gems >= dustCost && !dustAvailable) //hvis spilleren har nok Gems og der ikke er Dust tilgængelig allerede
        {
            CollectDustButton.SetActive(true);
            dustAvailable = true;
        }
        else if (!dustAvailable) //hvis ikke der er Dust tilgængelig allerede 
        {
            makeCurrencyErrorText.addonText = "Gems";
            makeCurrencyErrorText.messagePos = this.transform.position;
            makeCurrencyErrorText.newMessage();
        }
    }
    public void CollectDust()
    {
        //denne metode bliver kaldt når spilleren trykker på CollectDustButton
        dustAvailable = false; //spilleren kan nu trykke på ForgeDust button igen og forge mere dust
        CollectDustButton.SetActive(false);
        tempPlayerEconomy.Gems -= dustCost;
        tempPlayerEconomy.Dust += dustAmount;
    }
}
