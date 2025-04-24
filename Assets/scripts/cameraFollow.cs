using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public Transform target;       // Ball
    public Vector3 offset = new Vector3(0, 15f, -10f);
    public float smoothSpeed = 5f;


    public void SetTarget(Transform t)
    {
        target = t;
    }
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        // Always look at the player
        transform.LookAt(target);
    }



}
