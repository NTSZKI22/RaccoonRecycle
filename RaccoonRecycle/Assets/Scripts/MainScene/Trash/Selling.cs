using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selling : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collider hits a gameobject with tag "Laser"
        //then the gameobject the script is attached to will be destroyed
        if (other.gameObject.tag == "PetBottle")
        {
            sellPB();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Box")
        {
            sellBox();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Glass")
        {
            sellGlass();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Battery")
        {
            sellBattery();
            Destroy(other.gameObject);
        }
        //
    }

    void sellPB()
    {
        Debug.Log("Petbottle");

    }

    void sellBox()
    {
        Debug.Log("Box");
    }

    void sellGlass()
    {
        Debug.Log("Glass");
    }

    void sellBattery()
    {
        Debug.Log("Battery");
    }
}
