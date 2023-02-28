using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    Selling sellingScript;
    Properties propertiesScript;
    DatabaseCommunication dataScript;
    IncomeNumbers incomeScript;

    float speed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sellingScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<Selling>();
        incomeScript = GameObject.FindGameObjectWithTag("SellingScript").GetComponent<IncomeNumbers>();
        dataScript = GameObject.FindGameObjectWithTag("DatabaseCommunication").GetComponent<DatabaseCommunication>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        propertiesScript = gameObject.GetComponent<Properties>();
        speed = propertiesScript.speed();
        if (other.gameObject.tag == "Collider_PB" && gameObject.tag == "PetBottle")
        {
            rb.velocity = new Vector2(0, speed);
        }

        if (other.gameObject.tag == "Collider_BX" && gameObject.tag == "Box")
        {
            rb.velocity = new Vector2(0, speed);
        }

        if (other.gameObject.tag == "Collider_GL" && gameObject.tag == "Glass")
        {
            rb.velocity = new Vector2(0, speed);
        }

        if (other.gameObject.tag == "Collider_BY" && gameObject.tag == "Battery")
        {
            rb.velocity = new Vector2(0, speed);
        }

        if (other.gameObject.tag == "DefSeller")
        {
            sellingScript.normalSelling();
            dataScript.earningIncrease(gameObject.tag, sellingScript.defaultValue);
            incomeScript.showIncome(sellingScript.defaultValue, other.gameObject.transform.position);
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Seller_PB" || other.gameObject.tag == "Seller_BX" || other.gameObject.tag == "Seller_GL" || other.gameObject.tag == "Seller_BY")
        { 
            sellingScript.soldTrash(propertiesScript.value());
            dataScript.earningIncrease(gameObject.tag, propertiesScript.value());
            incomeScript.showIncome(propertiesScript.value(), other.gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
