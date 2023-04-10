using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpot : MonoBehaviour
{
    public int guards;
  

    public void addGuard()
    {
        guards++;
    }
    public void removeGuard()
    {
        guards--;
    }
}
