using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    public Rigidbody2D rb2D;
    public Animator anim;

    void Start()
    {
        foreach (var pPoint in patrolPoints)
        {
            pPoint.SetParent(null);
        }
    }

    void Update()
    {
        // if the enemy far away the point, walk to point
        if (Mathf.Abs(transform.position.x - patrolPoints[currentPoint].position.x) > .2f)
        {
            if (transform.position.x < patrolPoints[currentPoint].position.x)
            {
                rb2D.velocity = new Vector2(moveSpeed, rb2D.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y);
                transform.localScale = Vector3.one;
            }

            if (transform.position.y < (patrolPoints[currentPoint].position.y -.5f) && rb2D.velocity.y < .1f)
            {
                rb2D.velocity = new Vector2(0f, jumpForce);
            }
        }
        else //if the enemy reach at the point walk to next point
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            currentPoint++;
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }   
        } 
        anim.SetFloat("speed", Mathf.Abs(rb2D.velocity.x));      
    }
}
