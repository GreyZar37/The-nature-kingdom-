using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAllies : MonoBehaviour
{
    public int gemsPrice;
    public TempPlayerEconomy tempPlayerEconomy;
    public Transform spawnPos;

    public GameObject ally;

    bool isClose;

    void spawnAlly()
    {
        if(tempPlayerEconomy.Gems > gemsPrice)
        {
            Instantiate(ally, spawnPos.position, Quaternion.identity);
            tempPlayerEconomy.Gems -= gemsPrice;
        }

      
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isClose)
        {
            spawnAlly();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isClose = false;
        }
    }
}
