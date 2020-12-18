using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    public Autofire player;
    private float totalHP;
    void Start()
    {
        player = GetComponentInParent<Autofire>();
        totalHP = player.hp;
    }

    
    void Update()
    {
        //change bar to match HP
        float barPercent = player.hp / totalHP;
        Vector3 barTranslate = new Vector3(barPercent, 1, 1);
        gameObject.transform.localScale = barTranslate;
    }
}
