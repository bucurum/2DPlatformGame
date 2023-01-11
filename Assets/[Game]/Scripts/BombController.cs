using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] float timeToExplode = .5f;
    public GameObject explosion;
    private float bombRechargeCounter;

    [SerializeField] float blastRange;
    public LayerMask destructibleLayer;

    void Update()
    {
        timeToExplode -= Time.deltaTime;

        if (timeToExplode <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);

            Collider2D[] objectsToRemove = Physics2D.OverlapCircleAll(transform.position, blastRange, destructibleLayer);

            if (objectsToRemove.Length > 0)
            {
                foreach (Collider2D collider2D in objectsToRemove)
                {
                    Destroy(collider2D.gameObject);
                }
            }
        }
    }



}
