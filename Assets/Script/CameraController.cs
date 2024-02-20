using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Offset = new(7, 8.1f, -1);
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(player.position.x + Offset.x, player.position.y + Offset.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + Offset.x, transform.position.y, transform.position.z);
    }
}
