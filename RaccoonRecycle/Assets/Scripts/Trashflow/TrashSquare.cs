using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSquare : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody2D rbody;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        rbody.velocity = new Vector2(speed,0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); 
    }

    // Update is called once per frame
    void Update()
    {
         rbody = this.GetComponent<Rigidbody2D>();
            rbody.velocity = new Vector2(0,speed);
        if(transform.position.x > 1)
        {
           
        }
         if(transform.position.y > 3.8f)
        {
            Destroy(this.gameObject);
        }
    }
}

