using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    //szükséges scriptek
    Selling sellingScript; //a currency-t kezelõ script
    Properties propertiesScript; //tulajdonságokat taralmazó scripthez a változó
    DatabaseCommunication dataScript; //az adatbázisból megkapott adatokat kezelõ script

    float speed; //sebessége az objektumnak
    Rigidbody2D rb; //fizikával rendelkezõ objektum változó


    void Start() //induláskor lefut
    {
        rb = GetComponent<Rigidbody2D>(); //rb-t deklaráljuk mint a jelenlegi fizikával rendelkezõ objektum
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>(); //a scriptet kiveszi az adott objektumból mint komponense
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>(); //a scriptet kiveszi az adott objektumból mint komponense
    }

    void OnTriggerEnter2D(Collider2D other) //akkor fut le, amikor a szemét objektum ütközik valamivel
    {
        propertiesScript = gameObject.GetComponent<Properties>(); //a scriptet kiveszi az adott objektumból mint komponense
        speed = propertiesScript.speed(); // a gyorsaság a propertiesScript.speed által adott érték
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
            dataScript.earningIncrease(gameObject.tag, sellingScript.defaultValue); //növeli az adott szeméttípussal szerzett bevételt
            Destroy(gameObject); //törli a szemét objektumot
        }

        if(other.gameObject.tag == "Seller_PB" || other.gameObject.tag == "Seller_BX" || other.gameObject.tag == "Seller_GL" || other.gameObject.tag == "Seller_BY") //ha a végleges sellerrel ütközik
        { 
            sellingScript.soldTrashType(propertiesScript.value()); //meghívja a sellingscript soldtrashtype metódusát átadva neki a value tolajdonságot az aktuális szeméttõl
            dataScript.earningIncrease(gameObject.tag, propertiesScript.value()); //növeli az adott szeméttípussal szerzett bevételt
            Destroy(gameObject); //törli a szemét objektumot
        }
    }
}
