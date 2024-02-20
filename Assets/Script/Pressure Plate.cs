using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool pressed = false;

    private readonly List<Collision2D> m_Colliders = new List<Collision2D>();
    private SpriteRenderer spriteRenderer;
    private Animator m_Animator;
    Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            m_Animator.SetBool("Pressed", true);
        }
        else
        {
            m_Animator.SetBool("Pressed", false);
        }

        if (m_Colliders.Count == 0)
        {
            pressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            pressed = true;
            m_Colliders.Add(collision);
            collision.gameObject.transform.parent = transform;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            if (m_Colliders.Contains(collision))
            {
                collision.gameObject.transform.parent = null;
                m_Colliders.Remove(collision);
            }
        }
    }
}
