using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    //Kamera objektum
    
    [SerializeField]
    private float zoomOutMin = 3;
    private float zoomOutMax = 6;
    //Minimum és maximum nagyítás

    [SerializeField]
    private SpriteRenderer mapRenderer;
    //A map rendere

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;
    //A map minimum és maximum magassága és szélessége, azaz x és y értékei.

    private Vector3 dragOrigin;
    //Egy 3 dimenziós változó mely a húzás eredeti helyét tartalmazza.
    

    //Azért használunk awaket, mert az csak akkor fut le egyszer, ha a script használatban van, ellenben a starttal, ami akkor is le fog futni egyszer ha a scriptet nem használjuk.
    void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;
        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    // Framenkénti frissítéseket tartalmazza.
    void Update()
    {
        panCamera();
    }

    private void panCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position = clampCamera(cam.transform.position + difference);
            Debug.Log($"origin {dragOrigin} newposition {cam.ScreenToWorldPoint(Input.mousePosition)} =difference {difference}");
        }

        if(Input.touchCount == 2)
        {
            Touch touchOne = Input.GetTouch(0);
            Touch touchTwo = Input.GetTouch(1);

            Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;
            Vector2 touchTwoPrev = touchTwo.position - touchTwo.deltaPosition;

            float prevMagnitude = (touchOnePrev - touchTwoPrev).magnitude;
            float currentMagnitude = (touchOne.position - touchTwo.position).magnitude;

            float touchDifference = currentMagnitude - prevMagnitude;
            Zoom(touchDifference * 0.01f);
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Zoom(float increment)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }

    private Vector3 clampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);


        return new Vector3(newX, newY, targetPosition.z);
    }
}
