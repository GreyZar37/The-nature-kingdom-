using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    //todo:
    //gør det muligt at gange enemyamount med en float og så rundt resultatet op

    [Header("Enemy types")]
    [SerializeField]
    string ignore;
    [SerializeField]
    GameObject[] EnemyTypes; //de forskellige enemies

    [Header("Enemy base damage")]
    [SerializeField]
    string ignore2;
    [SerializeField]
    int[] BaseDamage;

    [Header("Enemy base health points")]
    [SerializeField]
    string ignore3;
    [SerializeField]
    int[] BaseHP;

    [Header("Base enemy amount")]
    [SerializeField]
    string ignore5;
    [SerializeField]
    int[] BaseAmount;

    [Header("Scaling numbers (multiplied with wave number)")]
    [SerializeField]
    string ignore4;
    [SerializeField]
    int DmgScale, HPScale, EnemyAmountScale;

    [Header("Wave stuff")]
    [SerializeField]
    int waveAmount; //Hvor mange waves der er
    [SerializeField]
    int currentWave; //den wave som er igang
    [SerializeField]
    GameObject spawnPoint; //hvor enemys spawner

    Wave waveClass;
    Wave[] wave;
    Transform moddedSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        //i tilfælde af at en gulerod har ændret på currentWave værdien i inspectioren til noget der ikke er 0
        currentWave = 0;

        //E
        moddedSpawnPosition = spawnPoint.transform;

        //sætter array længde lig med mængden af waves
        wave = new Wave[waveAmount];

        for (int i = 0; i < wave.Length; i++)
        {
            //put hver wave class ind i én array
            //wave[i] = new Wave waveClass;
        }

        //udregner alle wave numrene
        for (int i = 0; i < waveAmount; i++) //i = wave nummer
        {
            for (int b = 0; b < EnemyTypes.Length; b++) //b = enemy nummer
            {
                //Debug.Log($"i is {i} b is {b} enemyamount is {wave[i].EnemyAmount[b]} baseamount is {BaseAmount[b]} enemyamountscale is {EnemyAmountScale}");
                //wave has not object reference
                wave[i].EnemyAmount[b] = (BaseAmount[b] + (i * EnemyAmountScale));
                wave[i].EnemyDmg[b] = (BaseDamage[b] + (i * DmgScale));
                wave[i].EnemyHP[b] = (BaseHP[b] + (i * HPScale));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnNextWave()
    {
        //spawn ud fra wave nummeret
        for (int b = 0; b < EnemyTypes.Length; b++) //for hver enemy type
        {
            //sætter numrene for enemytype b
            EnemyTypes[b].GetComponent<EnemyStats>().HP = wave[currentWave].EnemyHP[b];
            EnemyTypes[b].GetComponent<EnemyStats>().DMG = wave[currentWave].EnemyDmg[b];

            for (int i = 0; i < wave[currentWave].EnemyAmount[b]; i++) //for værdien af EnemyAmount instantiate den enemy type. Eks: EnemyTypes b = 2, enemy 2s EnemyAmount = 3, for loop kører 3 gange
            {
                //offsetter transform for at undgå at de spawner oven i hinanden
                moddedSpawnPosition.position = new Vector3((0.2f + moddedSpawnPosition.transform.position.x), 0);

                //spawn enemy
                Instantiate(EnemyTypes[b], moddedSpawnPosition); 
            }
        }

    }
}
public class Wave : MonoBehaviour
{
    public WaveManager manager;

    public int[] EnemyAmount; //hvor mange enemys der skal spawne af hvilken type

    public int[] EnemyDmg; //hvor meget damage hver type enemy har

    public int[] EnemyHP; //hvor mange health points hver type enemy har
}

//Graven for død kode er herunder

//if (i != 0)
//{
//    for (int b = 0; b < EnemyTypes.Length; b++)
//    {
//        wave[i].EnemyAmount[b] = (BaseAmount[b] + (i * EnemyAmountScale));
//        wave[i].EnemyDmg[b] = (BaseDamage[b] + (i * DmgScale));
//        wave[i].EnemyHP[b] = (BaseHP[b] + (i * HPScale));
//    }

//}
//            else
//{
//    for (int d = 0; d < EnemyTypes.Length; d++)
//    {
//        wave[i].EnemyAmount[d] = BaseAmount[d];
//        wave[i].EnemyDmg[d] = BaseDamage[d];
//        wave[i].EnemyHP[d] = BaseHP[d];

//    }
//}