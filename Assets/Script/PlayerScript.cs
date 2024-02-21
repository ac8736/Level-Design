using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpForce = 700.0f;
    public bool facingLeft = false;
    public float speed = 10.0f;

    private bool isCarrying = false;
    private float horizontalMovement;
    private bool jump = false;
    private bool m_Grounded = false;
    private bool m_gameOver = false;

    private GameObject m_HeldCrate;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isCarrying)
            {
                Vector2 direction;
                if (facingLeft)
                    direction = Vector2.left;
                else
                    direction = Vector2.right;
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 1.5f);
                // Debug.DrawRay(transform.position, direction, Color.red);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.gameObject.CompareTag("Box"))
                    {
                        hit.collider.gameObject.transform.parent = transform;
                        hit.collider.gameObject.transform.position = transform.GetChild(0).position;
                        hit.collider.gameObject.GetComponent<Crate>().isBeingCarried = true;
                        isCarrying = true;
                        m_HeldCrate = hit.collider.gameObject;
                        break;
                    }
                }
            }
            else
            {
                isCarrying = false;
                m_HeldCrate.GetComponent<Crate>().isBeingCarried = false;
                if (facingLeft)
                    m_HeldCrate.transform.position = transform.GetChild(1).position;
                else
                    m_HeldCrate.transform.position = transform.GetChild(2).position;
                m_HeldCrate.transform.parent = null;
                m_HeldCrate = null;
            }
        }
        CheckGameOver();
    }


    private void CheckGameOver()
    {
        if(transform.position.y < -(GameManager.Instance().CameraHeight / 2))
        {
            GameManager.Instance().GameOver();
            m_gameOver=true;
        }
    }

    private void FixedUpdate()
    {
        if (!m_gameOver)
        {
            m_Rigidbody.velocity = new Vector2(horizontalMovement * speed, m_Rigidbody.velocity.y);
            if (jump)
            {
                m_Rigidbody.AddForce(new Vector2(0, jumpForce));
                jump = false;
            }
        }
        else
        {
            m_Rigidbody.velocity = Vector2.zero;
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
    public bool isGround()
    {
        return m_Grounded;
    }
}
