using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeFiringSound : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip Fire;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (GameObject.Find("Orange Team Player").GetComponent<Autofire>().isFiring == true || GameObject.Find("Orange Team Player (1)").GetComponent<Autofire>().isFiring == true ||
            GameObject.Find("Orange Team Player (2)").GetComponent<Autofire>().isFiring == true || GameObject.Find("Orange Team Player (3)").GetComponent<Autofire>().isFiring == true ||
            GameObject.Find("Orange Team Player (4)").GetComponent<Autofire>().isFiring == true || GameObject.Find("Orange Team Player (5)").GetComponent<Autofire>().isFiring == true ||
            GameObject.Find("Orange Team Player (6)").GetComponent<Autofire>().isFiring == true || GameObject.Find("Orange Team Player (7)").GetComponent<Autofire>().isFiring == true ||
            GameObject.Find("Orange Team Player (8)").GetComponent<Autofire>().isFiring == true)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(Fire, 1F);
            }
        }


    }
}
