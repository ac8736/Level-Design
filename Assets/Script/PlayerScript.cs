using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce = 700.0f;
    public float speed = 10.0f;

    private float horizontalMovement;
    private bool jump = false;
    private bool facingLeft = false;
    private bool m_Grounded = false;

    private BoxCollider2D m_BoxCollider2D;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private Rigidbody2D m_Rigidbody;
    [SerializeField] private LayerMask m_Platform;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        m_Animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        float extraHeightText = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(m_BoxCollider2D.bounds.center, m_BoxCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, m_Platform);
        if (raycastHit.collider != null)
        {
            m_Grounded = true;
            m_Animator.SetBool("isJumping", false);
        }
        else
        {
            m_Grounded = false;
            m_Animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_Grounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        m_Rigidbody.velocity = new Vector2(horizontalMovement * speed, m_Rigidbody.velocity.y);
        if (jump)
        {
            m_Rigidbody.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }

    void Flip()
    {
        if (horizontalMovement < 0)
        {
            facingLeft = true;
        } 
        else if (horizontalMovement > 0)
        {
            facingLeft = false;
        }
        m_SpriteRenderer.flipX = facingLeft;
    }
}
