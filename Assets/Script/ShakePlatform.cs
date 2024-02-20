using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEngine;

public class ShakePlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Collider2D> m_Colliders = new List<Collider2D>();
    public PlayerScript playerScript;
    Vector3 startingPos;
    Vector3 randomPos;
    bool pressed;
    bool isCoroutineReady;

    public float _distance = 0.1f;

    public float shakeTime = 3f;

    void Start()
    {
        startingPos = transform.parent.position;
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
            
        }else
        {
            isCoroutineReady = true;
            StopAllCoroutines();
        }

        if (m_Colliders.Count == 0)
        {
            pressed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Player") && playerScript.isGround()) || collision.gameObject.CompareTag("Box"))
        {
            pressed = true;
            m_Colliders.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
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
            transform.parent.position = randomPos;
            yield return null;
        }
        transform.parent.position = startingPos;
        transform.parent.gameObject.SetActive(false);
        isCoroutineReady = true;
        
    }
}
