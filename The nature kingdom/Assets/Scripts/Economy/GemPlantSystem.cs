using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPlantSystem : MonoBehaviour
{
    List<GemPlant> gemPlants = new List<GemPlant>();

    int gemPlantsBought = 0;
    private int plantNum;
    [SerializeField]
    TempPlayerEconomy tempPlayerEconomy;
    [SerializeField]
    makeCurrencyErrorText makeCurrencyErrorText;

    //hvor meget Dust det koster at købe en plante
    public int plantCost;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            gemPlants.Add(this.transform.GetChild(i).GetComponent<GemPlant>());
            gemPlants[i].GemGrowAmount = tempPlayerEconomy.PlantGrowAmount;
            gemPlants[i].isPlantSpot = true;
            gemPlants[i].setup();
        }
        Debug.Log($"{gemPlants[2]}");
        Debug.Log($"{gemPlants.Count}");
    }

    public void buyGemPlant()
    {
        if (tempPlayerEconomy.Dust >= plantCost)
        {
            Debug.Log($"{gemPlants[2]}");
            Debug.Log($"{gemPlants.Count}");
            Debug.Log($"{gemPlantsBought}");
            //Spilleren har nok Dust til at købe en plante
            if (gemPlants.Count > gemPlantsBought)
            {
                gemPlants[gemPlantsBought].gameObject.SetActive(true);
                gemPlantsBought += 1;

                //Spilleren har købt en plante for plantCost antal Dust
                tempPlayerEconomy.Dust -= plantCost;                
            }
            else if (gemPlants.Count == gemPlantsBought)
            {
                //Alle plant spots are fyldt op
                makeCurrencyErrorText.addonText = "plant spots";
                makeCurrencyErrorText.messagePos = this.transform.position;
                makeCurrencyErrorText.newMessage();
            }
            else
            {
                Debug.Log($"This message should be impossible!");
            }
        }
        else
        {
            //Spilleren har ikke nok Dust til at købe en plante
            makeCurrencyErrorText.addonText = "Dust";
            makeCurrencyErrorText.messagePos = this.transform.position;
            makeCurrencyErrorText.newMessage();
        }
    }
    public void addNewGemPlantSpots()
    {
        //Kald denne method hvis nye GemPlant gameobjekter bliver instanciate'd under spillet eller PlantGrowAmount ændres
        //Nye GemPlant gameobjekter *skal* have dette objekt som parent

        for (int i = 0; i < this.transform.childCount; i++)
        {
            //Nulstil listen
            gemPlants.Clear();
            //Tilføj alle GemPlant
            gemPlants.Add(this.transform.GetChild(i).GetComponent<GemPlant>());
            gemPlants[i].GemGrowAmount = tempPlayerEconomy.PlantGrowAmount;
            gemPlants[i].isPlantSpot = true;
        }
    }
}
