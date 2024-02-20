using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShakableText : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshPro;
    private Transform player;
    private bool coroutineStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 5f && !coroutineStarted)
        {
            StartCoroutine(FadeTextToZeroAlpha(1.0f, m_TextMeshPro));
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        coroutineStarted = true;
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        Destroy(i.gameObject);
    }
}
