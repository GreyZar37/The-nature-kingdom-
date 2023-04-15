using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotEnoughCurrency : MonoBehaviour
{
    string ErrorTextBase = "Not enough";
    public string ErrorTextAddon;

    [SerializeField] TextMeshProUGUI notEnoughTxt;
    [SerializeField] RectTransform rect;

    public Vector3 thisCoolPos;    

    // Start is called before the first frame update
    void Start()
    {
        notEnoughTxt.enabled = false;
    }
    public void MakeMessage()
    {        
        //anim�r tekst der siger 'not enough gems!'
        float timer = 0.4f;
        StartCoroutine(NotEnoughGemsAni(timer));
    }

    //man kan g�re det her p� mange andre m�der men nu havde jeg lige lyst til at bruge IEnumerator
    private IEnumerator NotEnoughGemsAni(float timer)
    {
        //skal vente p� n�ste update()
        yield return null;
        //g�r teksten synlig
        notEnoughTxt = this.GetComponent<TextMeshProUGUI>();
        notEnoughTxt.enabled = true;

        rect.localPosition = thisCoolPos;

        notEnoughTxt.text = ErrorTextBase + ErrorTextAddon;

        //rykker teksten op ad y-aksen med 7f hvert timer sekund
        for (int i = 0; i < 4; i++)
        {
            rect.localPosition += new Vector3(0f, 7f);
            yield return new WaitForSeconds(timer);
        }
        Destroy(this.gameObject);
    }
}
