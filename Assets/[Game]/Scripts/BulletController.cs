using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public Rigidbody2D rb;
    public Vector2 moveDirection;
    public GameObject impactEffect;


    void Update()
    {
        rb.velocity = moveDirection * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
