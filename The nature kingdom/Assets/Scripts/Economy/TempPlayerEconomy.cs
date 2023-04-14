using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempPlayerEconomy : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;

    //holder styr p� hvor mange gems spilleren har. M� ikke v�re mindre end 0
    public int Gems;
    //Int der holder p� Gems v�rdien for at sammenligne Gems og GemsLast s� UI text kan opdateres n�r Gems v�rdien �ndrer sig
    int GemsLast;
    //holder styr p� hvor meget Dust spilleren har.M� ikke v�re mindre end 0
    public int Dust;
    //samme koncept med GemsLast
    int DustLast;

    [Header("Text Components")]
    [SerializeField]
    string ignore; //unity splitter underst�ende TextMeshProUGUI op i 2 'text components' headers hvis ikke der er det her variabel. Weird
    [SerializeField]
    TextMeshProUGUI GemCountText, DustCountText;

    [Header("GemDropAmount")]
    [SerializeField]
    string ignore1;
    //Hvor mange gems hver type plante giver. Husk at kalde GemPlantSystem.addNewGemPlantSpots(); n�r PlantGrowAmount �ndres
    public int PlantGrowAmount, WildPlantGrowAmount;

    private void Start()
    {
        GemCountText.text = Gems.ToString();
        GemsLast = Gems;
        DustCountText.text = Dust.ToString();
        DustLast = Dust;
    }
    private void Update()
    {
        //hvis v�rdien af Gems eller Dust har �ndret sig, opdater hver deres UI
        if (GemsLast != Gems)
        {

            GemCountText.text = Gems.ToString();
            GemsLast = Gems;
        }

        if (DustLast != Dust)
        {
            DustCountText.text = Dust.ToString();
            DustLast = Dust;
        }
    }
}
