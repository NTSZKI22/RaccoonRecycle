using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSorce;
    void Start()
    {
        audioSorce = gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        audioSorce.Play();
    }
}
