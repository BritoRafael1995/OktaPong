using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Missile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private float speed = 10, destroyTime = 1.5f;
    [SerializeField]private int damage = 2;
    private GameObject control;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed * Time.deltaTime;
        control = GameObject.Find("GameControl");
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        Player otherPlayer = enemy.GetComponent<Player>();
        if (otherPlayer != null)
        {
            otherPlayer.TookDamage(damage);
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Vector2 obsNormal = collision.contacts[0].normal;
            Vector3 dir = Vector2.Reflect(rb.velocity, obsNormal).normalized;

            rb.velocity = dir * speed * Time.deltaTime;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Player otherPlayer = collision.gameObject.GetComponent<Player>();
            if (otherPlayer != null)
            {
                otherPlayer.TookDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        control.GetComponent<GameControl>().ChangeTurn();
    }


}
