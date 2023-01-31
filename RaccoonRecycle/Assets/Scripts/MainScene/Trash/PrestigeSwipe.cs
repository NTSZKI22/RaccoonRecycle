using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrestigeSwipe : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) //akkor fut le, amikor a szemét objektum ütközik valamivel
    {
        if (other.gameObject.tag == "PetBottle" || other.gameObject.tag == "Box" || other.gameObject.tag == "Glass" || other.gameObject.tag == "Battery")
        {

        }
    }

}
