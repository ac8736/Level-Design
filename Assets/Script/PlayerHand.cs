using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = transform.parent.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 direction;
            if (playerScript.facingLeft)
                direction = Vector2.left;
            else
                direction = Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.7f);
            Debug.DrawRay(transform.position, direction, Color.red);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Box"))
                {
                    Debug.Log("Hit");
                }
            }
        }
        Vector2 direction2;
        if (playerScript.facingLeft)
            direction2 = Vector2.left;
        else
            direction2 = Vector2.right;
        Debug.DrawRay(transform.position, direction2, Color.red);
    }
}
