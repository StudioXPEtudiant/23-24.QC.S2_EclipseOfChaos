using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Cube"); // The player
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 10);
    }
}
