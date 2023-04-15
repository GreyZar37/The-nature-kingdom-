using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DustForger : MonoBehaviour
{
    [TextArea][SerializeField] string notes;

    //hvor mange Gems det koster at forge dust
    public int dustCost;

    //hvor meget dust spilleren f�r
    public int dustAmount;

    //om der er dust tilg�ngeligt for spilleren at samle op
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
        if (tempPlayerEconomy.Gems >= dustCost && !dustAvailable) //hvis spilleren har nok Gems og der ikke er Dust tilg�ngelig allerede
        {
            CollectDustButton.SetActive(true);
            dustAvailable = true;
        }
        else if (!dustAvailable) //hvis ikke der er Dust tilg�ngelig allerede 
        {
            makeCurrencyErrorText.addonText = "Gems";
            makeCurrencyErrorText.messagePos = this.transform.position;
            makeCurrencyErrorText.NewMessage();
        }
    }
    public void CollectDust()
    {
        //denne metode bliver kaldt n�r spilleren trykker p� CollectDustButton
        dustAvailable = false; //spilleren kan nu trykke p� ForgeDust button igen og forge mere dust
        CollectDustButton.SetActive(false);
        tempPlayerEconomy.Gems -= dustCost;
        tempPlayerEconomy.Dust += dustAmount;
    }
}
