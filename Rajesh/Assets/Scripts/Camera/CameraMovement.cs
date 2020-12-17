using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform player;
    CameraShake cameraShake;
    public float smoothSpeed;
    public Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("CameraFollower").GetComponent<Transform>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
    }
   
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            cameraShake.Shake(0);
        }    
    }
    */

    private void FixedUpdate()
    {
        //poruszanie kamery za graczem

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, player.position + offset, smoothSpeed * Time.fixedDeltaTime);
        Vector3 buffer = transform.position;
        buffer.x = player.position.x;
        buffer.y = smoothedPosition.y;
        transform.position = buffer;
    }


}
