using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyATower : MonoBehaviour
{
    [TextAreaAttribute(10, 20)][SerializeField] string notes;

    public int towerCost; //Gems
    int towersBought; //nummer af towers spilleren har købt indtil videre
    [SerializeField] GameObject TowerPrefab; //Prefab til et Tower
    [SerializeField] TempPlayerEconomy tempPlayerEconomy;
    [SerializeField] makeCurrencyErrorText makeCurrencyErrorText;
    List<Transform> towerSpots = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            towerSpots.Add(this.transform.GetChild(i).transform);
        }
    }
    public void buyTower()
    {
        if (tempPlayerEconomy.Gems >= towerCost)
        {
            //Spilleren har nok Gems til at købe et tower
            if (towerSpots.Count > towersBought)
            {
                Instantiate(TowerPrefab, towerSpots[towersBought]);
                towersBought += 1;

                //Spilleren bruger Gems på at købe et Tower
                tempPlayerEconomy.Gems -= towerCost;
            }
            else if (towerSpots.Count == towersBought)
            {
                //Alle tower spots er fyldt op
                makeCurrencyErrorText.addonText = "tower spots";
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
            //Spilleren har ikke nok Gems til at købe et tower
            makeCurrencyErrorText.addonText = "Gems";
            makeCurrencyErrorText.messagePos = this.transform.position;
            makeCurrencyErrorText.newMessage();
        }
    }
    public void addNewTowerSpots()
    {
        //Kald denne metode hvis nye Tower spots bliver tilføjet under spillet

        //nulstil listen
        towerSpots.Clear();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            towerSpots.Add(this.transform.GetChild(i).transform);
        }
    }
}
