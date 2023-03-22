using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
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

    Wave[] wave;

    // Start is called before the first frame update
    void Start()
    {
        //i tilfælde af at en gulerod har ændret på currentWave værdien i inspectioren til noget der ikke er 0
        currentWave = 0;

        //sætter array længde lig med mængden af waves
        wave = new Wave[waveAmount];

        //udregner alle wave numrene
        for (int i = 0; i < waveAmount; i++) //i = wave nummer
        {
            for (int b = 0; b < EnemyTypes.Length; b++) //b = enemy nummer
            {
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
    private void SpawnNextWave()
    {
        //spawn ud fra wave nummeret
        for (int b = 0; b < EnemyTypes.Length; b++)
        {
            for (int i = 0; i < wave[currentWave].EnemyAmount[b]; i++)
            {

            }
        }
        
    }
}
public class Wave : MonoBehaviour
{
    public WaveManager manager;

    public int[] EnemyAmount;

    public int[] EnemyDmg;

    public int[] EnemyHP;
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