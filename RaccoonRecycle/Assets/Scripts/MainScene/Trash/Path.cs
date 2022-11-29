using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public int speed; //ideiglenes véltozó, sebessége az objektumnak
    Rigidbody2D rb; //fizikával rendelkezõ objektum változó

    Selling sellingScript;

    void Start() //induláskor lefut
    {
        rb = GetComponent<Rigidbody2D>(); //rb-t deklaráljuk mint a jelenlegi fizikával rendelkezõ objektum
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
    }

    void OnTriggerEnter2D(Collider2D other) //akkor fut le, amikor a szemét objektum ütközik valamivel
    {
        
        if (other.gameObject.tag == "Collider_PB" && gameObject.tag == "PetBottle")//ha Collider_PB-vel ütközik PetBottle
        {
            rb.velocity = new Vector2(0, speed); //irányt vált
        }

        if (other.gameObject.tag == "Collider_BX" && gameObject.tag == "Box") //ha Collider_BX-vel ütközik Box
        {
            rb.velocity = new Vector2(0, speed); //irányt vált
        }

        if (other.gameObject.tag == "Collider_GL" && gameObject.tag == "Glass") //ha Collider_GL-vel ütközik Glass
        {
            rb.velocity = new Vector2(0, speed); //irányt vált
        }

        if (other.gameObject.tag == "Collider_BY" && gameObject.tag == "Battery") //ha Collider_BY-vel ütközik Battery
        {
            rb.velocity = new Vector2(0, speed); //irányt vált
        }

        if (other.gameObject.tag == "DefSeller") //ha a defseller-el ütközik
        {
            sellingScript.normalSelling(); //meghívja a sellingscript normalselling metódusát
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Seller_PB")
        {
            sellingScript.sellingPB();
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Seller_BX")
        {
            sellingScript.sellingBX();
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Seller_GL")
        {
            sellingScript.sellingGL();
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Seller_BY")
        {
            sellingScript.sellingBY();
            Destroy(gameObject);
        }
    }
}
