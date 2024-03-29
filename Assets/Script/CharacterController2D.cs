using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    //[Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;           // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;   // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = true;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.

    const float k_GroundedRadius = .09f; // Radius of the overlap circle to determine if grounded
    [SerializeField] public bool m_Grounded;            // Whether or not the player is grounded.
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }

    private void FixedUpdate()
    {
        ////bool wasGrounded = m_Grounded;
        ////m_Grounded = false;

        //// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        //// This can be done using layers instead but Sample Assets will not overwrite your project settings.
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        //for (int i = 0; i < colliders.Length; i++)
        //{
        //    if (colliders[i].gameObject != gameObject)
        //    {
        //        //Debug.Log(wasGrounded);
        //        m_Grounded = true;
        //        OnLandEvent.Invoke();
        //        //Debug.Log(m_Grounded);
        //        //if (!wasGrounded)
        //        //    OnLandEvent.Invoke();
        //    }
        //}
    }

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                //Debug.Log(wasGrounded);
                m_Grounded = true;
                OnLandEvent.Invoke();
                //Debug.Log(m_Grounded);
                //if (!wasGrounded)
                //    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move,bool jump)
    {
        if (m_Grounded || m_AirControl)
        {     
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
