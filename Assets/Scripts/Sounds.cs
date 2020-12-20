using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip Fire;

    public AudioClip Dead;

    public AudioClip Dead2;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (GameObject.Find("Blue Team Player").GetComponent<Autofire>().isFiring == true || GameObject.Find("Blue Team Player (1)").GetComponent<Autofire>().isFiring == true
            || GameObject.Find("Blue Team Player (2)").GetComponent<Autofire>().isFiring == true || GameObject.Find("Blue Team Player (3)").GetComponent<Autofire>().isFiring == true
            || GameObject.Find("Blue Team Player (4)").GetComponent<Autofire>().isFiring == true)
            

        {
            if (audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(Fire, 1F);
            }
        }

        if (GameObject.Find("Blue Team Player").GetComponent<Autofire>().isDead == true || GameObject.Find("Blue Team Player (1)").GetComponent<Autofire>().isDead == true
           || GameObject.Find("Blue Team Player (2)").GetComponent<Autofire>().isDead == true || GameObject.Find("Blue Team Player (3)").GetComponent<Autofire>().isDead == true
           || GameObject.Find("Blue Team Player (4)").GetComponent<Autofire>().isDead == true)

            if (audioSource.isPlaying == false)
            {
                audioSource.PlayOneShot(Dead, 1F);
            }

    }
}
