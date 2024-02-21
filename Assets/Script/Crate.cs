using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public bool isBeingCarried = false;
    private Transform player;
    private Rigidbody2D m_Rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingCarried)
        {
            gameObject.layer = 0;
            transform.position = player.GetChild(0).position;
            m_Rigidbody2D.gravityScale = 0;

        }
        else
        {
            gameObject.layer = 7;
            m_Rigidbody2D.gravityScale = 1;
        }
    }
}
