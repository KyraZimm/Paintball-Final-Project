using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueFireSound : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip Fire;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (GameObject.Find("Blue Team Player").GetComponent<Autofire>().isFiring == true || GameObject.Find("Blue Team Player (1)").GetComponent<Autofire>().isFiring == true ||
            GameObject.Find("Blue Team Player (2)").GetComponent<Autofire>().isFiring == true || GameObject.Find("Blue Team Player (3)").GetComponent<Autofire>().isFiring == true ||
            GameObject.Find("Blue Team Player (4)").GetComponent<Autofire>().isFiring == true)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(Fire, 1F);
            }
        }


    }
}
