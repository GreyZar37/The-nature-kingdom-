using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int HP;
    public int DMG;

    public void Die()
    {
        Destroy(this);
    }
}

