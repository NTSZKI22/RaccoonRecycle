using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create : MonoBehaviour
{
    
    public Rigidbody2D trashPrefab; //pbulikus változó a létrehozni kívánt szemétnek
    public GameObject parent; //publikus változó a szemét szülõobjektumához
    
    //ideiglenes változók a szemét mûködéséhez
    public int frequency;
    public int speed;
    public int value;

    private Vector2 location; //vector2 változó egy helyzethez
    private Rigidbody2D rb; //fizikával rendelkezõ objektum változó

    void Start() //metódus lefut a játék indulásakor
    {
        location = GameObject.Find("Generator").transform.position; //location változó megkapja a Generator objektum helyzetét adatként
        Spawn(); //meghívja a spawn metódust
        StartCoroutine(Flow()); //meghívja a flow metódust
    }

    void Update() //minden képrissitésnél lefut
    {

    }

    private void Spawn() //metódus, lefutásával létrehoz szemét objektumokat
    { 
        Rigidbody2D rb = Instantiate(trashPrefab) as Rigidbody2D; //létrehoz egy rigidbody2d-t a trashPrefab-ból
        rb.transform.position = location; // rb helyzete a generator helyzete lesz
        rb.transform.parent = parent.transform; //rb szülõ objektumát beállítja
        rb.isKinematic = true; //nemtudom pontosan mit csinál, de kell hogy mûködjön, függ tõle az objektum fizikája
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
