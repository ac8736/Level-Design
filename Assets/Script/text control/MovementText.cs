using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementText : MonoBehaviour
{
    public bool movement;
    public bool jump;

    TextMeshProUGUI m_TextMeshPro;

    private void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(FadeTextToZeroAlpha(0.5f, m_TextMeshPro));
            }
        } 
        else if (jump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(FadeTextToZeroAlpha(0.5f, m_TextMeshPro));
            }
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        Destroy(i.gameObject);
    }
}
