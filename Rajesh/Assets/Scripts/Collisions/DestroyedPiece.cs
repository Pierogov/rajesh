using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedPiece : MonoBehaviour
{
    Rigidbody2D rb;
    bool once;
    PlayerMovement player;
    public GameObject full;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(10, 14);
        Physics2D.IgnoreLayerCollision(14, 12);

        if (!once) 
        {
            rb.AddForce(CheckSide(full.GetComponent<DestructionScript>().destructor) * Vector2.right * Random.Range(400f, 600f)/100, ForceMode2D.Impulse);
            once = true;
        }
    }

    int CheckSide(GameObject toCheck)
    {
        Transform buffer = toCheck.transform;
        if (toCheck.CompareTag("Bullet")) 
        {
            //Debug.Log("b");
            //Debug.Log(buffer.eulerAngles.z);
            if (buffer.eulerAngles.z >= 0 && buffer.eulerAngles.z <= 180)
            {
                return 1;
            }
            else
            {
                return -1;
            } 
        }
        else if (toCheck.CompareTag("Player"))
        {
            //Debug.Log("p");
            return (int)player.NormalizeValue(player.gameObject.transform.localScale.x);
        }
        else
        {
            //Debug.Log("e");
            return 1;
        }

    }
}
