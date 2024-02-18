using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public Animator animator;
    public float xdirection;
    public float xspeed;
    public float maxSpeed;

    public float accel;
    public float friction;
    public float jumpForce;

    public bool isFacingLeft;
    Vector2 facingRight;
    Vector2 facingLeft;

    [SerializeField] private Transform m_GroundCheck;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        accel = 1.5f;
        maxSpeed = 10f;
        friction = 1.5f;
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        facingRight = new Vector2(transform.localScale.x, transform.localScale.y);
        isFacingLeft = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            xdirection = -1;
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            xdirection = 1;
        }
        else
        {
            xdirection = 0;
        }
        if (xspeed < maxSpeed && (maxSpeed * -1) < xspeed && xdirection != 0)
        {
            xspeed += (xdirection * accel);
            if(xspeed > 0 && isFacingLeft)
            {
                Flip() ;
            }
            if(xspeed < 0 && !isFacingLeft)
            {
                
                Flip();
            }
        }

        if (xdirection == 0)
        {
            if (xspeed > 0)
            {
                xspeed -= friction;
            }
            if (xspeed < 0)
            {
                xspeed += friction;
            }
        }
        animator.SetFloat("Speed", Mathf.Abs(xspeed));
        myRigidbody.velocity = new Vector2(xspeed, myRigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            myRigidbody.AddForce(new Vector2(myRigidbody.velocity.x, jumpForce));

        }

    }

    void Flip()
    {
        isFacingLeft = !isFacingLeft;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
