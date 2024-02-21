using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool pressed = false;

    private readonly List<Collision2D> m_Colliders = new List<Collision2D>();
    private Vector3 m_OriginalPos;
    private readonly float distance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        m_OriginalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed)
        {
            if (Mathf.Abs(transform.position.y - m_OriginalPos.y) < distance)
            {
                transform.Translate(0, -0.01f, 0);
            }
        }
        else
        {
            if (transform.position.y < m_OriginalPos.y)
            {
                transform.Translate(0, 0.01f, 0);
            }
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
