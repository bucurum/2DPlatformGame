using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    public static RespawnController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //when we reload or load the next scene don`t destoy the player, so player don`t loose its progress 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Vector3 respawnPoint;
    [SerializeField] float waitToRespawn;
    private GameObject player;

    void Start()
    {
        player = PlayerHealthController.instance.gameObject;

        respawnPoint = player.transform.position;
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine()); 
    }

    IEnumerator RespawnCoroutine()
    {
        player.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        player.transform.position = respawnPoint;
        
        player.SetActive(true);
        PlayerHealthController.instance.FillHeath();
    }
}
