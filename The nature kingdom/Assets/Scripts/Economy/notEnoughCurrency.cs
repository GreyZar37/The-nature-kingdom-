using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class notEnoughCurrency : MonoBehaviour
{
    string ErrorTextBase = "Not enough";
    public string ErrorTextAddon;

    TextMeshProUGUI notEnoughTxt;

    public Vector3 thisCoolPos;

    RectTransform rect;

    // Start is called before the first frame update
    void Start()
    {
        notEnoughTxt = this.GetComponent<TextMeshProUGUI>();
        rect = this.GetComponent<RectTransform>();
        notEnoughTxt.enabled = false;
    }
    public void MakeMessage()
    {        
        //animér tekst der siger 'not enough gems!'
        float timer = 0.4f;
        StartCoroutine(notEnoughGemsAni(timer));
    }

    //man kan gøre det her på mange andre måder men nu havde jeg lige lyst til at bruge IEnumerator
    private IEnumerator notEnoughGemsAni(float timer)
    {
        //skal vente på næste update()
        yield return null;
        //gør teksten synlig
        notEnoughTxt = this.GetComponent<TextMeshProUGUI>();
        notEnoughTxt.enabled = true;

        rect.localPosition = thisCoolPos;
        //Debug.Log($"{rect.localPosition}");

        notEnoughTxt.text = ErrorTextBase + ErrorTextAddon;

        //rykker teksten op ad y-aksen med 7f hvert timer sekund
        for (int i = 0; i < 4; i++)
        {
            rect.localPosition += new Vector3(0f, 7f);
            //Debug.Log($"modded {rect.localPosition}");
            yield return new WaitForSeconds(timer);
        }
        Destroy(this.gameObject);
    }
}
