using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    Properties propertiesScript; //tulajdonságokat taralmazó scripthez a változó

    public Rigidbody2D trashPrefab; //pbulikus változó a létrehozni kívánt szemétnek
    public GameObject parent; //publikus változó a szemét szülõobjektumához
    
    //ideiglenes változók a szemét mûködéséhez
    float frequency;
    float speed;

    private Vector2 location; //vector2 változó egy helyzethez
    private Rigidbody2D rb; //fizikával rendelkezõ objektum változó

    void Start() //metódus lefut a játék indulásakor
    {
         //location változó megkapja a Generator objektum helyzetét adatként
        propertiesScript = trashPrefab.GetComponent<Properties>(); //a tulajdonságokat tartalmazó script az aktuális trashprefab
        
        Spawn(); //meghívja a spawn metódust
        StartCoroutine(Flow()); //meghívja a flow metódust

    }

    void Update() //minden képrissitésnél lefut
    {

    }

    private void Spawn() //metódus, lefutásával létrehoz szemét objektumokat
    {
        location = GameObject.Find("Generator").transform.position;
        Rigidbody2D rb = Instantiate(trashPrefab) as Rigidbody2D; //létrehoz egy rigidbody2d-t a trashPrefab-ból
        propertiesScript.defProperties();
        frequency = propertiesScript.frequency();
        speed = propertiesScript.speed();
        rb.transform.position = location; // rb helyzete a generator helyzete lesz
        rb.transform.SetParent(parent.transform); //rb szülõ objektumát beállítja
        rb.velocity = new Vector2(speed, 0); //rb mozgása: megindul ebbe az irányba
    }
    
    IEnumerator Flow() //metódus, lefutásával folyamatos a szemetek létrehozása
    {
        while (true) //végtelen ciklus
        {
            yield return new WaitForSeconds(frequency); //vár annyi másodpercet, amennyi frequency értéke
            Spawn(); //meghívja a spawn metódust
        }

    }
}
