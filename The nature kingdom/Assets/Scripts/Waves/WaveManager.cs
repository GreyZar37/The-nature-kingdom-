using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    //todo:
    //g�r det muligt at gange enemyamount med en float og s� rundt resultatet op
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
    GameObject waveGO, CoolWaves;
    [SerializeField]
    float EnemySpawnPosOffset; //forskellen i transforms for hver instanciated enemy
    [SerializeField]
    float minXPos, maxXPos, minXPos2, maxXPos2; //minimum og maximum x position som enemies m� spawne i

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

    //den position som hver ny enemy spawner p�
    Vector3 moddedSpawnPosition;
    Vector3 moddedSpawnPosition2;

    //den area som position skal genereres for (venstre eller h�jre)
    bool area;

    //alle wave scripts   
    List<Wave> wave = new List<Wave>();

    //gemt spawnpos. ved ikke hvorfor instanciate �ndrer p� SpawnPoint transform'en?
    Vector3 SpawnPointSave;

    // Start is called before the first frame update
    void Start()
    {
        //i tilf�lde af at en gulerod har �ndret p� currentWave v�rdien i inspectioren til noget der ikke er 0
        currentWave = 0;

        //s�tter start spawnpoint som udgangspunkt for at spawne enemies
        moddedSpawnPosition = new Vector3(spawnPoint.transform.position.x, 0);
        //gemmer spawnpoint s� vi er sikker p� at spawnpoint x-koordinaterne ikke �ndrer sig n�r programmet k�rer
        SpawnPointSave = new Vector3(spawnPoint.transform.position.x, 0);

        //spawner en wave per waveAmount
        for (int i = 0; i < waveAmount; i++)
        {
            Instantiate(waveGO, CoolWaves.transform);
            wave.Add(CoolWaves.transform.GetChild(i).GetComponent<Wave>());
            Debug.Log($"wave {i} is {wave[i]}");
        }

        //udregner alle wave numrene
        for (int i = 0; i < waveAmount; i++) //i = wave nummer
        {
            for (int b = 0; b < EnemyTypes.Length; b++) //b = enemy nummer
            {
                /*
                COOL DEBUGGING SKABELON FOR COOL PROGRAMM�RE
                Debug.Log($"i is {i} b is {b} enemyamount is {wave[i].EnemyAmount[b]} baseamount is {BaseAmount[b]} enemyamountscale is {EnemyAmountScale}");                
                Debug.Log($"i is {i}");
                Debug.Log($"b is {b}");
                Debug.Log($"wave[] is {wave[i]}");
                Debug.Log($"enemyamount is {wave[i].EnemyAmount[b]}");
                Debug.Log($"baseamount is {BaseAmount[b]}");
                Debug.Log($"enemyamountscale is {EnemyAmountScale}");
                */

                wave[i].EnemyAmount[b] = (BaseAmount[b] + (i * EnemyAmountScale));
                wave[i].EnemyDmg[b] = (BaseDamage[b] + (i * DmgScale));
                wave[i].EnemyHP[b] = (BaseHP[b] + (i * HPScale));
            }
        }
    }
    public void SpawnNextWave()
    {
        //kun instanciate n�ste wave n�r alle enemies fra den sidste wave er d�de, og der er flere waves tilbage
        if (spawnPoint.transform.childCount == 0 && currentWave <= waveAmount)
        {
            //nulstiller positionen som enemies spawner p�
            moddedSpawnPosition = new Vector3(SpawnPointSave.x, 0);
            //Debug.Log($"moddedspawn is (before for loop) {moddedSpawnPosition}");

            //spawn ud fra wave nummeret
            //Skal tilf�je scale numre til enemies
            if (currentWave != 0)
            {
                //for hver enemy type
                for (int b = 0; b < EnemyTypes.Length; b++)
                {
                    //s�tter numrene for enemytype b
                    EnemyTypes[b].GetComponent<EnemyStats>().HP = (wave[currentWave].EnemyHP[b] * (HPScale * currentWave));
                    EnemyTypes[b].GetComponent<EnemyStats>().DMG = (wave[currentWave].EnemyDmg[b] * (DmgScale * currentWave));

                    for (int i = 0; i < (wave[currentWave].EnemyAmount[b] * (EnemyAmountScale * currentWave)); i++)
                    /*
                    for v�rdien af EnemyAmount * (EnemyAmountScale * currentWave) instantiate den enemy type.
                    Eks:
                    EnemyTypes b = 2,
                    enemy 2s EnemyAmount = 3,
                    = for loop k�rer 3 gange
                    */
                    {
                        //Oleg siger at position skal v�re mellem two minX og maxX???????????++

                        //random om det er h�jre eller venstre area som denne enemys position ligger i
                        area = (UnityEngine.Random.value > 0.5f);
                        /*                        
                        M�l: Random v�rdi mellem banens minimum x-koordinater og maximum x-koordinater, hvor det er usandsynligt at den samme v�rdi fremkommer flere gange

                        random.range mellem minXPos og maxXPos + moddedSpawnPosition.x
                        Overst�ende v�rdi bliver clamp'ed mellem minXPos og maxXpos
                        Herefter minusses random.range mellem minXPos og maxXPos
                        V�rdien bliver igen clamp'ed mellem minXPos og maxXPos
                        */

                        if (area)
                        {
                            moddedSpawnPosition = new Vector3(Math.Clamp(Math.Clamp((UnityEngine.Random.Range(minXPos, maxXPos) + moddedSpawnPosition.x), minXPos, maxXPos) - UnityEngine.Random.Range(minXPos, maxXPos), minXPos, maxXPos), 0);

                            //Hvis moddedSpawnPosistion har form�et at v�re enten minXPos eller maxXPos...
                            if (moddedSpawnPosition.x == minXPos)
                            {
                                moddedSpawnPosition.x += UnityEngine.Random.Range(1, -(maxXPos + 1));
                            }
                            else if (moddedSpawnPosition.x == maxXPos)
                            {
                                moddedSpawnPosition.x += UnityEngine.Random.Range(1, -(maxXPos + 1));
                            }
                            Debug.Log($"moddedspawnposition is {moddedSpawnPosition.x}");
                            //Debug.Log($"moddedspawn is {moddedSpawnPosition}");

                            //spawn enemy
                            Instantiate(EnemyTypes[b], moddedSpawnPosition, Quaternion.Euler(0, 0, 0), spawnPoint.transform);

                            //Omdanner transform til recttransform. Set hvis enemies bruger transform i stedet for recttransform
                            spawnPoint.transform.GetChild(spawnPoint.transform.childCount - 1).GetComponent<RectTransform>().localPosition = moddedSpawnPosition;
                        }
                        else
                        {
                            moddedSpawnPosition2 = new Vector3(Math.Clamp(Math.Clamp((UnityEngine.Random.Range(minXPos2, maxXPos2) + moddedSpawnPosition2.x), minXPos2, maxXPos2) - UnityEngine.Random.Range(minXPos2, maxXPos2), minXPos2, maxXPos2), 0);

                            //Hvis moddedSpawnPosistion2 har form�et at v�re enten minXPos2 eller maxXPos2...
                            if (moddedSpawnPosition2.x == minXPos2)
                            {
                                moddedSpawnPosition2.x -= UnityEngine.Random.Range(1, (maxXPos2 - 1));
                            }
                            else if (moddedSpawnPosition2.x == maxXPos2)
                            {
                                moddedSpawnPosition2.x -= UnityEngine.Random.Range(1, (maxXPos2 - 1));
                            }
                            Debug.Log($"moddedspawnposition2 is {moddedSpawnPosition2.x}");
                            //Debug.Log($"moddedspawn is {moddedSpawnPosition}");

                            //spawn enemy
                            Instantiate(EnemyTypes[b], moddedSpawnPosition2, Quaternion.Euler(0, 0, 0), spawnPoint.transform);

                            //Omdanner transform til recttransform. Set hvis enemies bruger transform i stedet for recttransform
                            spawnPoint.transform.GetChild(spawnPoint.transform.childCount - 1).GetComponent<RectTransform>().localPosition = moddedSpawnPosition2;
                        }
                    }
                }
            }
            //Skal ikke tilf�je scale numre til enemies
            else
            { //for hver enemy type
                for (int b = 0; b < EnemyTypes.Length; b++)
                {
                    //s�tter numrene for enemytype b
                    EnemyTypes[b].GetComponent<EnemyStats>().HP = wave[currentWave].EnemyHP[b];
                    EnemyTypes[b].GetComponent<EnemyStats>().DMG = wave[currentWave].EnemyDmg[b];

                    for (int i = 0; i < wave[currentWave].EnemyAmount[b]; i++)
                    /*
                    for v�rdien af EnemyAmount instantiate den enemy type.
                    Eks:
                    EnemyTypes b = 2,
                    enemy 2s EnemyAmount = 3,
                    = for loop k�rer 3 gange
                    */
                    {
                        //random om det er h�jre eller venstre area som denne enemys position ligger i
                        area = (UnityEngine.Random.value > 0.5f);
                        /*                        
                        M�l: Random v�rdi mellem banens minimum x-koordinater og maximum x-koordinater, hvor det er usandsynligt at den samme v�rdi fremkommer flere gange

                        random.range mellem minXPos og maxXPos + moddedSpawnPosition.x
                        Overst�ende v�rdi bliver clamp'ed mellem minXPos og maxXpos
                        Herefter minusses random.range mellem minXPos og maxXPos
                        V�rdien bliver igen clamp'ed mellem minXPos og maxXPos
                        */

                        if (area)
                        {
                            moddedSpawnPosition = new Vector3(Math.Clamp(Math.Clamp((UnityEngine.Random.Range(minXPos, maxXPos) + moddedSpawnPosition.x), minXPos, maxXPos) - UnityEngine.Random.Range(minXPos, maxXPos), minXPos, maxXPos), 0);

                            //Hvis moddedSpawnPosistion har form�et at v�re enten minXPos eller maxXPos...
                            if (moddedSpawnPosition.x == minXPos)
                            {
                                moddedSpawnPosition.x += UnityEngine.Random.Range(1, -(maxXPos + 1));
                            }
                            else if (moddedSpawnPosition.x == maxXPos)
                            {
                                moddedSpawnPosition.x += UnityEngine.Random.Range(1, -(maxXPos + 1));
                            }
                            Debug.Log($"moddedspawnposition is {moddedSpawnPosition.x}");
                            //Debug.Log($"moddedspawn is {moddedSpawnPosition}");

                            //spawn enemy
                            Instantiate(EnemyTypes[b], moddedSpawnPosition, Quaternion.Euler(0, 0, 0), spawnPoint.transform);

                            //Omdanner transform til recttransform. Set hvis enemies bruger transform i stedet for recttransform
                            spawnPoint.transform.GetChild(spawnPoint.transform.childCount - 1).GetComponent<RectTransform>().localPosition = moddedSpawnPosition;
                        }
                        else
                        {
                            moddedSpawnPosition2 = new Vector3(Math.Clamp(Math.Clamp((UnityEngine.Random.Range(minXPos2, maxXPos2) + moddedSpawnPosition2.x), minXPos2, maxXPos2) - UnityEngine.Random.Range(minXPos2, maxXPos2), minXPos2, maxXPos2), 0);

                            //Hvis moddedSpawnPosistion2 har form�et at v�re enten minXPos2 eller maxXPos2...
                            if (moddedSpawnPosition2.x == minXPos2)
                            {
                                moddedSpawnPosition2.x -= UnityEngine.Random.Range(1, (maxXPos2 - 1));
                            }
                            else if (moddedSpawnPosition2.x == maxXPos2)
                            {
                                moddedSpawnPosition2.x -= UnityEngine.Random.Range(1, (maxXPos2 - 1));
                            }
                            Debug.Log($"moddedspawnposition2 is {moddedSpawnPosition2.x}");
                            //Debug.Log($"moddedspawn is {moddedSpawnPosition}");

                            //spawn enemy
                            Instantiate(EnemyTypes[b], moddedSpawnPosition2, Quaternion.Euler(0, 0, 0), spawnPoint.transform);

                            //Omdanner transform til recttransform. Set hvis enemies bruger transform i stedet for recttransform
                            spawnPoint.transform.GetChild(spawnPoint.transform.childCount - 1).GetComponent<RectTransform>().localPosition = moddedSpawnPosition2;
                        }
                    }
                }
                //klar til n�ste wave
                currentWave += 1;
            }
        }
    }
}

/*
//Graven for d�d kode er herunder

if (i != 0)
{
    for (int b = 0; b < EnemyTypes.Length; b++)
{
        wave[i].EnemyAmount[b] = (BaseAmount[b] + (i * EnemyAmountScale));
        wave[i].EnemyDmg[b] = (BaseDamage[b] + (i * DmgScale));
        wave[i].EnemyHP[b] = (BaseHP[b] + (i * HPScale));
    }

}
            else
{
    for (int d = 0; d < EnemyTypes.Length; d++)
    {
        wave[i].EnemyAmount[d] = BaseAmount[d];
        wave[i].EnemyDmg[d] = BaseDamage[d];
        wave[i].EnemyHP[d] = BaseHP[d];

    }
}
//det her er dumt

                    else if (i == 0)
                    {
                        Debug.Log($"i is {i}");
                        Debug.Log($"childcount is {spawnPoint.transform.childCount}");
                        Debug.Log($"child is {i + (spawnPoint.transform.childCount - 1)}");
                        spawnPoint.transform.GetChild(i + (spawnPoint.transform.childCount - 1)).GetComponent<RectTransform>().localPosition = moddedSpawnPosition.position;
                    }
                    else
                    {
                        Debug.Log($"else");
                        Debug.Log($"i is {i}");
                        Debug.Log($"childcount is {spawnPoint.transform.childCount}");
                        Debug.Log($"child is {i + spawnPoint.transform.childCount}");
                        spawnPoint.transform.GetChild(i + spawnPoint.transform.childCount).GetComponent<RectTransform>().localPosition = moddedSpawnPosition.position;
                    }
                    
                    if (spawnPoint.transform.childCount >= 3)
                    {
                        spawnPoint.transform.GetChild(i + spawnPoint.transform.childCount).GetComponent<RectTransform>().localPosition = moddedSpawnPosition.position;
                    }
                    else
                    {
                        spawnPoint.transform.GetChild(i).GetComponent<RectTransform>().localPosition = moddedSpawnPosition.position;
                    }

//E

RectTransform rectPos = new RectTransform();                    
                    rectPos = (RectTransform)moddedSpawnPosition;
                    Debug.Log($"rectpos is {rectPos}");
                   
//Gammel spawn koordinater

public void SpawnNextWave()
    {
        if (spawnPoint.transform.childCount == 0 && currentWave <= waveAmount) //kun spawn n�ste wave n�r alle enemies fra den sidste wave er d�de, og der er flere waves at spawne
        {
            //nulstiller positionen som enemies spawner p�
            moddedSpawnPosition = new Vector3(SpawnPointSave.x, 0);
            //Debug.Log($"moddedspawn is (before for loop) {moddedSpawnPosition}");

            //spawn ud fra wave nummeret
            if (currentWave != 0) //Skal tilf�je scale numre til enemies
            {
                for (int b = 0; b < EnemyTypes.Length; b++) //for hver enemy type
                {
                    //s�tter numrene for enemytype b
                    EnemyTypes[b].GetComponent<EnemyStats>().HP = (wave[currentWave].EnemyHP[b] * (HPScale * currentWave));
                    EnemyTypes[b].GetComponent<EnemyStats>().DMG = (wave[currentWave].EnemyDmg[b] * (DmgScale * currentWave));

                    for (int i = 0; i < (wave[currentWave].EnemyAmount[b] * (EnemyAmountScale * currentWave)); i++)
                    /*
                    for v�rdien af EnemyAmount * (EnemyAmountScale * currentWave) instantiate den enemy type.
                    Eks:
                    EnemyTypes b = 2,
                    enemy 2s EnemyAmount = 3,
                    = for loop k�rer 3 gange
                    
{
    //offsetter transform for at undg� at de spawner oven i hinanden
    if (i != 0)
    {
        moddedSpawnPosition = new Vector3(((EnemySpawnPosOffset * i) + moddedSpawnPosition.x), 0);
    }
    else
    {
        moddedSpawnPosition = new Vector3((EnemySpawnPosOffset + moddedSpawnPosition.x), 0);
    }

    //Debug.Log($"moddedspawn is {moddedSpawnPosition}");

    //spawn enemy
    Instantiate(EnemyTypes[b], moddedSpawnPosition, Quaternion.Euler(0, 0, 0), spawnPoint.transform);

    //Omdanner transform til recttransform. Set hvis enemies bruger transform i stedet for recttransform
    spawnPoint.transform.GetChild(spawnPoint.transform.childCount - 1).GetComponent<RectTransform>().localPosition = moddedSpawnPosition;

}
                }
            }
            else //Skal ikke tilf�je scale numre til enemies
{
    for (int b = 0; b < EnemyTypes.Length; b++) //for hver enemy type
    {
        //s�tter numrene for enemytype b
        EnemyTypes[b].GetComponent<EnemyStats>().HP = wave[currentWave].EnemyHP[b];
        EnemyTypes[b].GetComponent<EnemyStats>().DMG = wave[currentWave].EnemyDmg[b];

        for (int i = 0; i < wave[currentWave].EnemyAmount[b]; i++)
        
        for v�rdien af EnemyAmount instantiate den enemy type.
        Eks:
        EnemyTypes b = 2,
        enemy 2s EnemyAmount = 3,
        = for loop k�rer 3 gange
        
        {
            //offsetter transform for at undg� at de spawner oven i hinanden
            if (i != 0)
            {
                moddedSpawnPosition = new Vector3(((EnemySpawnPosOffset * i) + moddedSpawnPosition.x), 0);
            }
            else
            {
                moddedSpawnPosition = new Vector3((EnemySpawnPosOffset + moddedSpawnPosition.x), 0);
            }

            //Debug.Log($"moddedspawn is {moddedSpawnPosition}");

            //spawn enemy
            Instantiate(EnemyTypes[b], moddedSpawnPosition, Quaternion.Euler(0, 0, 0), spawnPoint.transform);

            //Omdanner transform til recttransform. Set hvis enemies bruger transform i stedet for recttransform
            spawnPoint.transform.GetChild(spawnPoint.transform.childCount - 1).GetComponent<RectTransform>().localPosition = moddedSpawnPosition;

        }
    }
}
currentWave += 1;
        }
    }



*/