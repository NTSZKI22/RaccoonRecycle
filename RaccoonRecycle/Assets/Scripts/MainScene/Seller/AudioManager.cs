using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSorce; //az sfx forrása
    void Start()
    {
        audioSorce = gameObject.GetComponent<AudioSource>();//elkérjük az audió forrás komponensünket
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        audioSorce.Play();
    }
}
