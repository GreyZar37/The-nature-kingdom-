using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wavey : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;

    public int[] EnemyAmount; //hvor mange enemys der skal spawne af hvilken type

    public int[] EnemyDmg; //hvor meget damage hver type enemy har

    public int[] EnemyHP; //hvor mange health points hver type enemy har
    
}
