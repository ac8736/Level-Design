using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public List<PressurePlate> m_Plates;
    public string m_NextScene;

    private bool m_IsOpen = false;
    private Animator m_Animator;
    private bool canOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        canOpen = CheckAllPlatesPressed();

        if (canOpen && !m_IsOpen)
        {
            m_IsOpen = true;
            m_Animator.SetTrigger("Open");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canOpen && collision.gameObject.CompareTag("Player"))
        {
            if (m_NextScene != "")
            {
                SceneManager.LoadScene(m_NextScene);
            }
            else
            {
                Debug.LogWarning("We didn't add a next scene for the door!");
            }
        }
    }

    private bool CheckAllPlatesPressed()
    {
        foreach (var plate in m_Plates)
        {
            if (!plate.pressed)
            {
                if (m_IsOpen)
                {
                    m_IsOpen = false;
                    m_Animator.SetTrigger("Close");
                }
                return false;
            }
        }
        return true;
    }
}
