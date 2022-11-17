using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    
    public Rigidbody2D trashPrefab;
    public GameObject parent;
    

    public int frequency;
    public int speed;
    public int value;

    private Vector2 location;
    private Rigidbody2D rb;

    void Start()
    {
        location = GameObject.Find("Generator").transform.position;
        Spawn();
        StartCoroutine(Flow());
    }

    void Update()
    {

    }

    private void Spawn()
    {
        //GameObject a = Instantiate(trashPrefab) as GameObject;
        Rigidbody2D b = Instantiate(trashPrefab) as Rigidbody2D;
        b.transform.position = location;
        b.transform.parent = parent.transform;
        rb = b; //.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.isKinematic = true;
        rb.velocity = new Vector2(speed, 0);
    }
    
    IEnumerator Flow()
    {
        while (true)
        {
            yield return new WaitForSeconds(frequency);
            Spawn();
        }

    }

    
}
