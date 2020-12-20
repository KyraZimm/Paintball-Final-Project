using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSounds : MonoBehaviour
{
    public AudioSource audioSource;
    
    void Start()
    {
        
    }

    
    void Update()
    {

        if (GameObject.Find("Orange Team Player").GetComponent<Autofire>().isDead == true || GameObject.Find("Orange Team Player (1)").GetComponent<Autofire>().isDead == true
       || GameObject.Find("Orange Team Player (2)").GetComponent<Autofire>().isDead == true || GameObject.Find("Orange Team Player (3)").GetComponent<Autofire>().isDead == true
       || GameObject.Find("Orange Team Player (4)").GetComponent<Autofire>().isDead == true || GameObject.Find("Orange Team Player (5)").GetComponent<Autofire>().isDead == true
       || GameObject.Find("Orange Team Player (6)").GetComponent<Autofire>().isDead == true || GameObject.Find("Orange Team Player (7)").GetComponent<Autofire>().isDead == true
       || GameObject.Find("Orange Team Player (8)").GetComponent<Autofire>().isDead == true)

            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
    }
}
