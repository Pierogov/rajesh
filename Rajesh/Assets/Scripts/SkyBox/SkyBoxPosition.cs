using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxPosition : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();   
    }

    void Update()
    {
        //ruszanie skyboxa na pozycję gracza w osi x
        transform.position = new Vector3(player.transform.position.x, transform.position.y, 0f);
    }
}
