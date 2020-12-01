using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Transform player;
    public float smoothSpeed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("CameraFollower").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, player.position, smoothSpeed * Time.fixedDeltaTime);
        Vector3 buffer = transform.position;
        buffer.x = player.position.x;
        buffer.y = smoothedPosition.y;
        transform.position = buffer;
    }
}
