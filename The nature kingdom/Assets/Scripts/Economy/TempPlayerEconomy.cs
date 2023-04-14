using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempPlayerEconomy : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;

    //holder styr på hvor mange gems spilleren har. Må ikke være mindre end 0
    public int Gems;
    //Int der holder på Gems værdien for at sammenligne Gems og GemsLast så UI text kan opdateres når Gems værdien ændrer sig
    int GemsLast;
    //holder styr på hvor meget Dust spilleren har.Må ikke være mindre end 0
    public int Dust;
    //samme koncept med GemsLast
    int DustLast;

    [Header("Text Components")]
    [SerializeField]
    string ignore; //unity splitter understående TextMeshProUGUI op i 2 'text components' headers hvis ikke der er det her variabel. Weird
    [SerializeField]
    TextMeshProUGUI GemCountText, DustCountText;

    [Header("GemDropAmount")]
    [SerializeField]
    string ignore1;
    //Hvor mange gems hver type plante giver. Husk at kalde GemPlantSystem.addNewGemPlantSpots(); når PlantGrowAmount ændres
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
        //hvis værdien af Gems eller Dust har ændret sig, opdater hver deres UI
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
