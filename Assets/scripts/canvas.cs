using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvas : MonoBehaviour
{
    public Transform target; // the ball
    private Transform cam;
    public float len = .1f;
    public float zOffset = 1;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Offset position above the ball and forward/backward in Z direction
        Vector3 offset = new Vector3(0, len, zOffset);
        transform.position = target.position + offset;

        // Billboard toward camera (ignore Y for flat rotation)
        Vector3 direction = transform.position - cam.position;
        direction.y = 0;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }



   
}
