using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public Animator anim;
    [SerializeField] float distanceToOpen;
    private PlayerMovementHandler player;
    private bool playerExiting;
    public Transform exitPoint;
    [SerializeField] float movePlayerSpeed;
    [SerializeField] bool isGoingToNextScene;
    [SerializeField] bool isGoingToPreviousScene;
    

    void Start()
    {
        player = PlayerHealthController.instance.GetComponent<PlayerMovementHandler>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceToOpen)
        {
            anim.SetBool("doorOpen", true);
        }
        else
        {
            anim.SetBool("doorOpen", false);
        }
        if (playerExiting)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, exitPoint.position, movePlayerSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerExiting)
            {
                player.canMove = false;

                StartCoroutine(UseDoor());
            }
        }
    }

    IEnumerator UseDoor()
    {
        playerExiting = true;
        player.anim.enabled = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(1.5f);
    
        RespawnController.instance.SetSpawnPoint(exitPoint.position);
        player.canMove = true;
        player.anim.enabled = true;

        UIController.instance.StartFadeToNormal();

        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int previousIndex = SceneManager.GetActiveScene().buildIndex - 1;

        if (isGoingToNextScene)
        {
            SceneManager.LoadScene(nextIndex); 
        }
        else if (isGoingToPreviousScene)
        {
            SceneManager.LoadScene(previousIndex); 
        }
    }
    //git Commit
}
