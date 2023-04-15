using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPlant : MonoBehaviour
{
    [TextArea][SerializeField] string notes;

    [Header("Public")]
    //Hvor mange gems planten gror. Public s� amount kan g�res st�rre af andre scripts
    public int GemGrowAmount;
    //Det variabel som bestemmer hvad grow timeren nulstiller tilbage til n�r den er f�rdig
    public float GrowTimerNum;

    [Header("Private")]
    //Dette er child objektet "GemButton"
    [SerializeField] GameObject CollectGemButton;
    [SerializeField] TempPlayerEconomy tempPlayerEconomy;

    //Det variabel som t�ller ned (Time.Deltatime bliver minusset fra dette variabel)
    float GrowTimer;
    //Om planten er igang med at gro Gems eller ej
    bool IsGrowingGems = true;
    //om planten er et plantspot (inden i basen) eller en plante uden for basen
    public bool isPlantSpot;

    // Start is called before the first frame update
    void Start()
    {
        GrowTimer = GrowTimerNum;
        CollectGemButton.SetActive(false);
        tempPlayerEconomy = FindObjectOfType<TempPlayerEconomy>();
    }

    // Update is called once per frame
    void Update()
    {
        //Timer der s�rger for at der g� GrowTimerNum sekunder f�r n�ste Gems kan opsamles af spilleren
        //Man kan ogs� bruge ienumerator her
        if (IsGrowingGems)
        {
            GrowTimer -= Time.deltaTime;

            if (GrowTimer <= 0)
            {
                IsGrowingGems = false;
                //G�r gem grafik button objekter aktive her
                CollectGemButton.SetActive(true);
            }
        }
    }
    public void PlantGemsCollected()
    {
        //Der bliver tjekket om GrowTimerNum har �ndret sig
        GrowTimer = GrowTimerNum;
        CollectGemButton.SetActive(false);
        //Plante kan nu gro nye Gems
        IsGrowingGems = true;

        if (isPlantSpot)
        {
            tempPlayerEconomy.Gems += GemGrowAmount;
        }
        else
        {
            tempPlayerEconomy.Gems += tempPlayerEconomy.WildPlantGrowAmount;
        }
    }
}
