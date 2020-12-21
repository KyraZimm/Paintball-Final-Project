using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterScript : MonoBehaviour
{
    public int enemies;
    private Text enemyCounter;
    
    void Start()
    {
        enemyCounter = GetComponent<Text>();
        enemies = 9;
    }
    
    void Update()
    {
        enemyCounter.text = "Enemy Counter : " + enemies;
    }
}
