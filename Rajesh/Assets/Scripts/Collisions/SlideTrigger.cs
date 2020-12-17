using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTrigger : MonoBehaviour
{
    PlayerMovement player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter");
        if (collision.CompareTag("Player"))
        {
            player.sliding = true;
            player.slidingUnder = true;
            Debug.Log("EEnter");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Leave");
        player.slidingUnder = false;
        if (collision.CompareTag("Player"))
        {
            if (player.slideTime <= 0)
            {
                player.sliding = false;
                player.slidingUnder = false;
                Debug.Log("LLeave");
            }
        }
    }
}
