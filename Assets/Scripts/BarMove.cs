using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarMove : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0 , 0.8f, 0);
    

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.position = player.transform.position + offset;
    }
}
