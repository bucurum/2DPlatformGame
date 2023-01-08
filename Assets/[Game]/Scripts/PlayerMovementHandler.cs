using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    public Transform groundPoint;
    private bool isGrounded;
    public LayerMask groundLayer;
    public Animator anim;
    public BulletController shotToFire;
    public Transform shotPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //in this code we get player movement with "ad or arrow keys"
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, rb.velocity.y); //there is 2 GetAxis method, GetAxis and GetAxisRaw, the diffrence between two is GetAxis is wait a little time and add movement to player movement, GetAxisRaw is when you press the keys the player move instantly
    
        //handle direction change
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
        }


        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("speed",Mathf.Abs(rb.velocity.x));

    }
}
