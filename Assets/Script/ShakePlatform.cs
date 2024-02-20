using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEngine;

public class ShakePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Collision2D> m_Colliders = new List<Collision2D>();
    public GameObject m_platform;
    private PlayerScript playerScript;
    Vector3 startingPos;
    Vector3 randomPos;
    bool pressed;
    bool isCoroutineReady;

    public float _distance = 0.1f;

    public float shakeTime = 3f;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        startingPos = m_platform.transform.position;
        pressed = false;
        isCoroutineReady = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(pressed)
        {
            if(isCoroutineReady)
            {
                isCoroutineReady=false;
                StopAllCoroutines();
                StartCoroutine(Shake());
            }
            
        }
        /*
        else
        {
            isCoroutineReady = true;
            StopAllCoroutines();
            m_platform.transform.position = startingPos;
        }*/

        if (m_Colliders.Count == 0)
        {
            pressed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") && playerScript.isGround()) || collision.gameObject.CompareTag("Box"))
        {
            pressed = true;
            m_Colliders.Add(collision);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
        {
            if (m_Colliders.Contains(collision))
            {
                m_Colliders.Remove(collision);
            }
        }
    }

    private IEnumerator Shake()
    {
        var whenDone = Time.time + shakeTime;
        while(Time.time < whenDone)
        {
            randomPos = startingPos + (Random.insideUnitSphere * _distance);
            m_platform.transform.position = randomPos;
            yield return null;
        }
        m_platform.transform.position = startingPos;
        m_platform.GetComponent<Rigidbody2D>().gravityScale = 3f;
        gameObject.SetActive(false);
        //transform.parent.gameObject.SetActive(false);
        isCoroutineReady = true;
        
    }
}
