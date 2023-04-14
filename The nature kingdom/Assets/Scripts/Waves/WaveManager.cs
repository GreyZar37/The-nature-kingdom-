using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyNumbers
{
    public GameObject enemyGO; //Enemy gameobject
    public int baseDamage;
    public int baseHP;
    public int baseAmount;
    public int baseGems;
}
public class WaveNumbers : MonoBehaviour
{
    public int[] EnemyAmount; //hvor mange enemys der skal spawne af hvilken type
    public int[] EnemyDmg; //hvor meget damage hver type enemy har
    public int[] EnemyHP; //hvor mange health points hver type enemy har    
    public int[] EnemyGems; //hvor mange gems hver type enemy giver
    private void Awake()
    {
        int _waveAmt = FindObjectOfType<WaveManager>().waveAmount;
        EnemyAmount = new int[_waveAmt];
        EnemyDmg = new int[_waveAmt];
        EnemyHP = new int[_waveAmt];
        EnemyGems = new int[_waveAmt];
    }
}
public class WaveManager : MonoBehaviour
{
    [TextAreaAttribute(10, 20)][SerializeField] string notes;

    [Header("Wave stuff")]
    public int waveAmount; //Hvor mange waves der er
    [SerializeField] int currentWave; //den wave som er igang
    [SerializeField] GameObject spawnPoint; //hvor enemys spawner

    //waveGO = Skabelonen for hvert wave objekt; CoolWaves = Parent objektet for nye waves
    [SerializeField] GameObject waveGO, coolWaves;
    [SerializeField] float leftMinXPos, leftMaxXPos, rightMinXPos, rightMaxXPos; //minimum og maksimum x position som enemies må instaciate i

    [Header("Enemies & their stats")]
    [SerializeField] EnemyNumbers[] enemies;

    [Header("Scaling numbers (multiplied with wave number & rounded to an int)")]
    [SerializeField] string ignore;
    [SerializeField] float dmgScale, hpScale, enemyAmountScale, gemsScale;

    //den position som hver ny enemy spawner på
    Vector3 moddedSpawnPosition;

    //alle wave scripts   
    List<WaveNumbers> waves = new();

    // Start is called before the first frame update
    void Start()
    {
        //i tilfælde af at nogen har ændret på currentWave værdien i inspectoren til noget der ikke er 0
        currentWave = 0;

        for (int i = 0; i < waveAmount; i++)
        {
            waves.Add(gameObject.AddComponent<WaveNumbers>());
        }

        //udregner alle wave numrene
        for (int _waveNummer = 0; _waveNummer < waveAmount; _waveNummer++)
        {
            for (int b = 0; b < enemies.Length; b++) //b = enemy nummer
            {
                //Den første wave er altid 0, så derfor er det ok at plusse med scale numrne her da 0 * nummer = 0, og alle waves efter den første skal have scale på
                waves[_waveNummer].EnemyAmount[b] = enemies[b].baseAmount + Mathf.RoundToInt(_waveNummer * enemyAmountScale);
                waves[_waveNummer].EnemyDmg[b] = enemies[b].baseDamage + Mathf.RoundToInt(_waveNummer * dmgScale);
                waves[_waveNummer].EnemyHP[b] = enemies[b].baseHP + Mathf.RoundToInt(_waveNummer * hpScale);
                waves[_waveNummer].EnemyGems[b] = enemies[b].baseGems + Mathf.RoundToInt(_waveNummer * gemsScale);
            }
        }
    }
    public void SpawnNextWave()
    {
        //kun instanciate næste wave når alle enemies fra den sidste wave er døde, og der er flere waves tilbage
        if (spawnPoint.transform.childCount == 0 && currentWave < waveAmount)
        {
            //for hver enemy type
            for (int i = 0; i < enemies.Length; i++)
            {
                //sætter numrene for enemytype b
                enemies[i].baseHP = waves[currentWave].EnemyHP[i];
                enemies[i].baseDamage = waves[currentWave].EnemyDmg[i];
                enemies[i].baseGems = waves[currentWave].EnemyGems[i];

                for (int j = 0; j < waves[currentWave].EnemyAmount[i]; j++)
                /*
                For værdien af EnemyAmount instantiate den enemy type.
                Eks:
                EnemyTypes b = 2,
                enemy 2s EnemyAmount = 3,
                = for loop kører 3 gange
                */
                {
                    SpawnAtRandomPos(i);
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
            moddedSpawnPosition = new Vector3(UnityEngine.Random.Range(leftMinXPos, leftMaxXPos), -3);
        }
        else
        {
            //Random position
            moddedSpawnPosition = new Vector3(UnityEngine.Random.Range(rightMinXPos, rightMaxXPos), -3);
        }
        //Instanciater denne enemy type på moddedSpawnPosition og sætter det antal gems som den kan give
        Instantiate(enemies[j].enemyGO, moddedSpawnPosition, Quaternion.Euler(0, 0, 0), spawnPoint.transform).GetComponent<CanDropGems>().GemDropAmount = waves[currentWave].EnemyGems[j];
    }
}