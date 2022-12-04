using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    //szükséges scriptek
    Selling sellingScript;
    Properties propertiesScript;
    DatabaseCommunication dataScript;

    float speed; //sebessége az objektumnak
    Rigidbody2D rb; //fizikával rendelkezõ objektum változó


    void Start() //induláskor lefut
    {
        rb = GetComponent<Rigidbody2D>(); //rb-t deklaráljuk mint a jelenlegi fizikával rendelkezõ objektum
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
    }

    void OnTriggerEnter2D(Collider2D other) //akkor fut le, amikor a szemét objektum ütközik valamivel
    {
        propertiesScript = gameObject.GetComponent<Properties>();
        speed = propertiesScript.speed();
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

            switch (gameObject.tag)
            {
                case "PetBottle":
                    dataScript.pbEarningsIncrease(sellingScript.defaultValue);
                    break;
                case "Box":
                    dataScript.bxEarningsIncrease(sellingScript.defaultValue);
                    break;
                case "Glass":
                    dataScript.glEarningsIncrease(sellingScript.defaultValue);
                    break;
                case "Battery":
                    dataScript.byEarningsIncrease(sellingScript.defaultValue);
                    break;
            }

            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Seller_PB" || other.gameObject.tag == "Seller_BX" || other.gameObject.tag == "Seller_GL" || other.gameObject.tag == "Seller_BY")
        {
            sellingScript.soldTrashType(propertiesScript.value());
            switch (gameObject.tag)
            {
                case "PetBottle":
                    dataScript.pbEarningsIncrease(propertiesScript.value());
                    break;
                case "Box":
                    dataScript.bxEarningsIncrease(propertiesScript.value());
                    break;
                case "Glass":
                    dataScript.glEarningsIncrease(propertiesScript.value());
                    break;
                case "Battery":
                    dataScript.byEarningsIncrease(propertiesScript.value());
                    break;
            }

            Destroy(gameObject);
        }
    }
}
