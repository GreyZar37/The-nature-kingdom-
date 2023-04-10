using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    [TextAreaAttribute(10, 20)]
    [SerializeField]
    string notes;

    [Header("Wave stuff")]
    [SerializeField]
    int waveAmount; //Hvor mange waves der er
    [SerializeField]
    int currentWave; //den wave som er igang
    [SerializeField]
    GameObject spawnPoint; //hvor enemys spawner
    //waveGO = Skabelonen for hvert wave objekt; CoolWaves = Parent objektet for nye waves
    [SerializeField]
    GameObject waveGO, coolWaves;
    [SerializeField]
    float leftMinXPos, leftMaxXPos, rightMinXPos, rightMaxXPos; //minimum og maksimum x position som enemies må spawne i

    [Header("Enemy types")]
    [SerializeField]
    GameObject[] enemyTypes; //de forskellige enemies

    [Header("Enemy base damage")]
    [SerializeField]
    int[] baseDamage;

    [Header("Enemy base health points")]
    [SerializeField]
    int[] baseHP;

    [Header("Base enemy amount")]
    [SerializeField]
    int[] baseAmount;

    [Header("Scaling numbers (multiplied with wave number & rounded to an int)")]
    [SerializeField]
    string ignore;
    [SerializeField]
    float dmgScale, hpScale, enemyAmountScale;

    //den position som hver ny enemy spawner på
    Vector3 moddedSpawnPosition;

    //alle wave scripts   
    List<Wave> wave = new List<Wave>();

    // Start is called before the first frame update
    void Start()
    {
        //i tilfælde af at en gulerod har ændret på currentWave værdien i inspectioren til noget der ikke er 0
        currentWave = 0;

        //spawner en wave per waveAmount
        for (int i = 0; i < waveAmount; i++)
        {
            Instantiate(waveGO, coolWaves.transform);
            wave.Add(coolWaves.transform.GetChild(i).GetComponent<Wave>());
            //Debug.Log($"wave {i} is {wave[i]}");
        }

        //udregner alle wave numrene
        for (int _waveNummer = 0; _waveNummer < waveAmount; _waveNummer++)
        {
            for (int b = 0; b < enemyTypes.Length; b++) //b = enemy nummer
            {
                //Den første wave er altid 0, så derfor er det ok at plusse med scale numrne her da 0 * nummer = 0, og alle waves efter den første skal have scale på
                wave[_waveNummer].EnemyAmount[b] = (baseAmount[b] + (Mathf.RoundToInt((float)_waveNummer * enemyAmountScale)));
                wave[_waveNummer].EnemyDmg[b] = (baseDamage[b] + (Mathf.RoundToInt((float)_waveNummer * dmgScale)));
                wave[_waveNummer].EnemyHP[b] = (baseHP[b] + (Mathf.RoundToInt((float)_waveNummer * hpScale)));
            }
        }
    }
    public void SpawnNextWave()
    {
        //kun instanciate næste wave når alle enemies fra den sidste wave er døde, og der er flere waves tilbage
        if (spawnPoint.transform.childCount == 0 && currentWave < waveAmount)
        {
            //spawn ud fra wave nummeret
            //Skal tilføje scale numre til enemies
            if (currentWave != 0)
            {
                //for hver enemy type
                for (int i = 0; i < enemyTypes.Length; i++)
                {
                    //sætter numrene for enemytype b
                    enemyTypes[i].GetComponent<EnemyStats>().HP = wave[currentWave].EnemyHP[i];
                    enemyTypes[i].GetComponent<EnemyStats>().DMG = wave[currentWave].EnemyDmg[i];

                    for (int j = 0; j < wave[currentWave].EnemyAmount[i]; j++)
                    /*
                    for værdien af EnemyAmount instantiate den enemy type.
                    Eks:
                    EnemyTypes b = 2,
                    enemy 2s EnemyAmount = 3,
                    = for loop kører 3 gange
                    */
                    {
                        SpawnAtRandomPos(i);
                    }
                }
            }
            //Skal ikke tilføje scale numre til enemies
            else
            { //for hver enemy type
                for (int i = 0; i < enemyTypes.Length; i++)
                {
                    //sætter numrene for enemytype b
                    enemyTypes[i].GetComponent<EnemyStats>().HP = wave[currentWave].EnemyHP[i];
                    enemyTypes[i].GetComponent<EnemyStats>().DMG = wave[currentWave].EnemyDmg[i];

                    for (int j = 0; j < wave[currentWave].EnemyAmount[i]; j++)
                    /*
                    for værdien af EnemyAmount instantiate den enemy type.
                    Eks:
                    EnemyTypes b = 2,
                    enemy 2s EnemyAmount = 3,
                    = for loop kører 3 gange
                    */
                    {
                        SpawnAtRandomPos(i);
                    }
                }
            }
            //klar til næste wave
            currentWave += 1;
        }
    }
    private void SpawnAtRandomPos(int j)
    {
        //random om det er højre eller venstre area som denne enemys position ligger i
        bool _area = (UnityEngine.Random.value > 0.5f);

        if (_area)
        {
            //Random position
            moddedSpawnPosition = new Vector3(UnityEngine.Random.Range(leftMinXPos, leftMaxXPos), 0);

            //Debug.Log($"moddedspawnposition is {moddedSpawnPosition.x}");
        }
        else
        {
            //Random position
            moddedSpawnPosition = new Vector3(UnityEngine.Random.Range(rightMinXPos, rightMaxXPos), 0);
        }
        //Instanciater denne enemy type på moddedSpawnPosition 
        Instantiate(enemyTypes[j], moddedSpawnPosition, Quaternion.Euler(0, 0, 0), spawnPoint.transform);

        //Omdanner transform til recttransform. Set hvis enemies bruger transform i stedet for recttransform
        spawnPoint.transform.GetChild(spawnPoint.transform.childCount - 1).GetComponent<RectTransform>().localPosition = moddedSpawnPosition;
    }
}