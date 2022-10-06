using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrash1 : MonoBehaviour
{
    public GameObject trashPrefab;
        public float respawnTime;
        private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
       screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)); 
       trashSpawn();
       StartCoroutine(trashFlow());
    }


    private void trashSpawn()
    {
            GameObject a = Instantiate(trashPrefab) as GameObject;
            a.transform.position = new Vector2(-6, 0);
    }
    // Update is called once per frame
    IEnumerator trashFlow()
    {
        while(true)
        {
            yield return new WaitForSeconds(respawnTime);
            trashSpawn();
        }
        
    }
}
