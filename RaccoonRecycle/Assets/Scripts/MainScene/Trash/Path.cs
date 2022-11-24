using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{

    //public GameObject ez;
    public int speed;

    void OnTriggerEnter2D(Collider2D other)
    {
        //if the player collider hits a gameobject with tag "Laser"
        //then the gameobject the script is attached to will be destroyed
        if (other.gameObject.tag == "PetBottle" && this.gameObject.tag == "Collider_PB")
        {
            //other.transform.velocity = new Vector2(0, speed);
        }
        if (other.gameObject.tag == "Box" && this.gameObject.tag == "Collider_BX")
        {
            
        }
        if (other.gameObject.tag == "Glass" && this.gameObject.tag == "Collider_GL")
        {
           
        }
        if (other.gameObject.tag == "Battery" && this.gameObject.tag == "Collider_BY")
        {
            
        }
        //
    }
}
