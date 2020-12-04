using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionTest : MonoBehaviour
{
    Rigidbody2D rb;

    bool m_oneTime = true;
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 11);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (m_oneTime)
            {
                rb.AddForce(Vector2.right * 3f, ForceMode2D.Impulse);
            }
        }
    }
}
