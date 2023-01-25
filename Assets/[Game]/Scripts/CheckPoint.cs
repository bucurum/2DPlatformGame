using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //TODO: whenever the player picks up the checkpoint and die, destroy the pickuped abilities
   void OnTriggerEnter2D(Collider2D other)
   {
        if (other.CompareTag("Player"))
        {
            RespawnController.instance.SetSpawnPoint(transform.position);
        }  
   }
}
