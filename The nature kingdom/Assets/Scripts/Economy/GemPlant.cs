using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class GemPlant : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;

    [Header("Public")]
    //Hvor mange gems planten gror. Public så amount kan gøres større af andre scripts
    public int GemGrowAmount;
    //Det variabel som bestemmer hvad grow timeren nulstiller tilbage til når den er færdig
    public float GrowTimerNum;

    [Header("Private")]
    //Dette er child objektet "GemButton"
    [SerializeField]
    GameObject CollectGemButton;
    [SerializeField]
    TempPlayerEconomy tempPlayerEconomy;

    //Det variabel som tæller ned (Time.Deltatime bliver minusset fra dette variabel)
    float GrowTimer;
    //Om planten er igang med at gro Gems eller ej
    bool IsGrowingGems = true;
    //om planten er et plantspot (inden i basen) eller en plante uden for basen
    public bool isPlantSpot;


    // Start is called before the first frame update
    void Start()
    {
        GrowTimer = GrowTimerNum;
        setup();
    }    

    // Update is called once per frame
    void Update()
    {
        //man kan også bruge ienumerator, men det er for nørder :)))
        if (IsGrowingGems)
        {
            GrowTimer -= Time.deltaTime;

            if (GrowTimer <= 0)
            {
                IsGrowingGems = false;
                //Gør gem grafik button objekter aktive her
                CollectGemButton.SetActive(true);
            }
        }
    }
    public void PlantGemsCollected()
    {
        //Der bliver tjekket om GrowTimerNum har ændret sig
        GrowTimer = GrowTimerNum;
        CollectGemButton.SetActive(false);
        //Plante kan nu gro nye Gems
        IsGrowingGems = true;

        //GIV SPILLER GemGrowAmount ANTAL GEMS HER
        //evt. gør dette til et unity event og fjern tempplayereconomy reference
        if (isPlantSpot)
        {
            tempPlayerEconomy.Gems += GemGrowAmount;
        }
        else
        {
            tempPlayerEconomy.Gems += tempPlayerEconomy.WildPlantGrowAmount;
        }
    }
    public void setup()
    {
        //denne method gør planten klar til start
        if (isPlantSpot)
        {
            CollectGemButton.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else
        {
            CollectGemButton.SetActive(false);
        }
    }
}
