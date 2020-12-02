using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmFix : MonoBehaviour
{
    public bool coliding;

    public Transform rotator;
    public Transform arm;
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        while (collision != null)
        {
            Debug.Log("collide");
            coliding = true;
            arm.RotateAround(rotator.position, Vector3.up, 20f);
        }
        coliding = false;
        
    }
}
