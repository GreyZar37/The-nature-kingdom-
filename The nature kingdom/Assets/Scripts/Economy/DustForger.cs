using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DustForger : MonoBehaviour
{
    [TextArea][SerializeField] string notes;

    //hvor mange Gems det koster at forge dust
    public int dustCost;

    //hvor meget dust spilleren får
    public int dustAmount;

    //om der er dust tilgængeligt for spilleren at samle op
    bool dustAvailable = false; 

    [Header("Components")]
    [SerializeField] TempPlayerEconomy tempPlayerEconomy;
    [SerializeField] GameObject CollectDustButton;
    [SerializeField] MakeCurrencyErrorText makeCurrencyErrorText;


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
            makeCurrencyErrorText.NewMessage();
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
