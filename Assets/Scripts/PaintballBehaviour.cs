using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballBehaviour : MonoBehaviour
{

    public GameObject target;
    private Rigidbody2D rb;
    public bool isPlayerFiring;

    private SpriteRenderer sprite;
    
  
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        float speed = 3f;
        float dx = target.transform.position.x - gameObject.transform.position.x;
        float dy = target.transform.position.y - gameObject.transform.position.y;
        Vector2 magnitude = new Vector2(dx, dy);
        rb.velocity = magnitude * speed;

        if (!isPlayerFiring)
        {
            sprite.color = new Color(.8f, .3f, .8f);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && isPlayerFiring)
        {
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Player" && !isPlayerFiring)
        {
            Destroy(gameObject);
        }
    }
}
