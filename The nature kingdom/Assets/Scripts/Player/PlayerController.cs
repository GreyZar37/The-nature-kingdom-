using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    string notes;

    [Header("Numbers")]
    public float playerSpeed;

    [Header("Components")]
    [SerializeField]
    Collider2D playerC; //Playercollider


    float playerSpriteScale;

    // Start is called before the first frame update
    void Start()
    {
        playerSpriteScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Fra https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        //Selve det der får spillere til at bevæge sig
        float xAxisMove = Input.GetAxis("Horizontal") * playerSpeed;

        //Gør xAxisMove for per sekund
        xAxisMove *= Time.deltaTime;

        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(new Vector2(xAxisMove, 0f));
        }

        //Næste 2 If statements fra https://www.youtube.com/watch?v=k-75tAys7iI
        //Flipper spriten når spilleren går til venstre eller højre. Sprite skal være vendt mod venstre fra start for at dette ikke ser mærkeligt ud.
        Vector3 charaScale = transform.localScale;

        if (Input.GetAxis("Horizontal") < 0) //Venstre
        {
            charaScale.x = playerSpriteScale;
        }
        else if (Input.GetAxis("Horizontal") > 0) //Højre
        {

            charaScale.x = -playerSpriteScale;
        }
        transform.localScale = charaScale;
    }
}
