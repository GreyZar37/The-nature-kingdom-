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
        //Selve det der f�r spillere til at bev�ge sig
        float xAxisMove = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(xAxisMove * playerSpeed, rb.velocity.y);


        //N�ste 2 If statements fra https://www.youtube.com/watch?v=k-75tAys7iI
        //Flipper spriten n�r spilleren g�r til venstre eller h�jre. Sprite skal v�re vendt mod venstre fra start for at dette ikke ser m�rkeligt ud.
        Vector3 charaScale = transform.localScale;

        if (xAxisMove < 0) //Venstre
        {
            charaScale.x = playerSpriteScale;
        }
        else if (xAxisMove > 0) //H�jre
        {

            charaScale.x = -playerSpriteScale;
        }
        transform.localScale = charaScale;


        anim.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));


       
    }

 
}
