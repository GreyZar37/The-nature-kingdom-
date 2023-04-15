using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPlantSystem : MonoBehaviour
{
    int gemPlantsBought = 0;
    [SerializeField] TempPlayerEconomy tempPlayerEconomy;
    [SerializeField] MakeCurrencyErrorText makeCurrencyErrorText;
    [SerializeField] GameObject gemPlantPrefab; //Prefab til gemplant
    List<Transform> PlantSpots = new List<Transform>();

    //hvor meget Dust det koster at købe en plante
    public int plantCost;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            PlantSpots.Add(this.transform.GetChild(i).transform);
        }
    }

    public void buyGemPlant()
    {
        if (tempPlayerEconomy.Dust >= plantCost)
        {
            //Spilleren har nok Dust til at købe en plante
            if (PlantSpots.Count > gemPlantsBought)
            {
                Instantiate(gemPlantPrefab, PlantSpots[gemPlantsBought]);
                gemPlantsBought += 1;

                //Spilleren har købt en plante for plantCost antal Dust
                tempPlayerEconomy.Dust -= plantCost;
            }
            else if (PlantSpots.Count == gemPlantsBought)
            {
                //Alle plant spots are fyldt op
                makeCurrencyErrorText.addonText = "plant spots";
                makeCurrencyErrorText.messagePos = this.transform.position;
                makeCurrencyErrorText.NewMessage();
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
            makeCurrencyErrorText.NewMessage();
        }
    }
    public void addNewGemPlantSpots()
    {
        //Kald denne metode hvis nye gemplant spots bliver tilføjet under spillet

        //nulstil listen
        PlantSpots.Clear();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            PlantSpots.Add(this.transform.GetChild(i).transform);
        }
    }
}
