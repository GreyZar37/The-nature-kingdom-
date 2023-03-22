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
    GameObject notEnoughGemsGO, CollectDustButton;

    TextMeshProUGUI notEnoughGemsTxt;
    bool dustAvailable = false; //om der er dust tilgængeligt for spilleren at samle op
    bool notEnoughGemsAniIsRunning = false; //om notEnoughGemsAni er i gang eller ej (ellers bliver der lavet nye notEnoughGemsAni som kører på samme tid)
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        notEnoughGemsTxt = notEnoughGemsGO.GetComponent<TextMeshProUGUI>();

        startPos = notEnoughGemsGO.transform.position;
        notEnoughGemsGO.SetActive(false);
        CollectDustButton.SetActive(false);
    }
    public void ForgeDust()
    {
        if (tempPlayerEconomy.Gems >= dustCost && !dustAvailable) //hvis spilleren har nok Gems og der ikke er Dust tilgængelig allerede
        {
            CollectDustButton.SetActive(true);
            dustAvailable = true;
        }
        else if (!dustAvailable && !notEnoughGemsAniIsRunning) //hvis ikke der er Dust tilgængelig allerede og notEnoughGemsAni ikke allerede kører
        {
            //animér tekst der siger 'not enough gems!'
            notEnoughGemsGO.SetActive(true);
            float timer = 0.4f;
            notEnoughGemsAniIsRunning = true;
            StartCoroutine(notEnoughGemsAni(timer));
        }
    }

    //man kan gøre det her på mange andre måder men nu havde jeg lige lyst til at bruge IEnumerator
    private IEnumerator notEnoughGemsAni(float timer)
    {
        //sætter positionen tilbage til start
        notEnoughGemsGO.transform.position = startPos;
        //rykker teksten op ad y-aksen med 0,3f hvert timer sekund
        for (int i = 0; i < 4; i++)
        {
            notEnoughGemsGO.transform.position += new Vector3(0f, 0.3f);
            yield return new WaitForSeconds(timer);
        }
        //teksten forsvinder og coroutine bliver stoppet efter animationen har kørt
        notEnoughGemsGO.SetActive(false);
        notEnoughGemsAniIsRunning = false;  
        StopCoroutine(notEnoughGemsAni(timer));
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
