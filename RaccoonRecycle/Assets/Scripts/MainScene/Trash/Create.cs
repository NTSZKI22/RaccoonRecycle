using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    Properties propertiesScript;

    public Rigidbody2D trashPrefab;
    public GameObject parent;
    
    float frequency;
    float speed;

    private Vector2 location;
    private Rigidbody2D rb;

    void Start()
    {
        
        
        Spawn();
        StartCoroutine(Flow());
    }

    private void Spawn()
    {
        propertiesScript = trashPrefab.GetComponent<Properties>();
        location = GameObject.Find("Generator").transform.position;
        Rigidbody2D rb = Instantiate(trashPrefab) as Rigidbody2D;
        propertiesScript.defProperties();
        frequency = propertiesScript.frequency();
        speed = propertiesScript.speed();
        rb.transform.position = location;
        rb.transform.SetParent(parent.transform);
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
