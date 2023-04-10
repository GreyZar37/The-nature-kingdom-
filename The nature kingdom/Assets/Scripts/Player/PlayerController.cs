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
    Rigidbody2D rb;
    Animator anim;

    float playerSpriteScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSpriteScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Fra https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        //Selve det der får spillere til at bevæge sig
        float xAxisMove = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(xAxisMove * playerSpeed, rb.velocity.y);


        //Næste 2 If statements fra https://www.youtube.com/watch?v=k-75tAys7iI
        //Flipper spriten når spilleren går til venstre eller højre. Sprite skal være vendt mod venstre fra start for at dette ikke ser mærkeligt ud.
        Vector3 charaScale = transform.localScale;

        if (xAxisMove < 0) //Venstre
        {
            charaScale.x = playerSpriteScale;
        }
        else if (xAxisMove > 0) //Højre
        {

            charaScale.x = -playerSpriteScale;
        }
        transform.localScale = charaScale;


        anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));


       
    }

 
}
