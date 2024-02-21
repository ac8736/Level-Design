using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public bool isBeingCarried = false;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingCarried)
        {
            gameObject.layer = 0;
            transform.position = player.GetChild(0).position;
        }
        else
        {
            gameObject.layer = 7;
        }
    }
}
