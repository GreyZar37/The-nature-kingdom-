using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GemPlant : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;

    //Hvor mange gems planten gror. Public så amount kan gøres større af andre scripts
    public int GemGrowAmount;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
