using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    /// Target of the camera
    public Transform target;
    public float smoothing;

    /// Minimum position of camera
    public float maxY, minY, maxX, minX;
    public float minPosition = -1.3f;

    /// Maximum position of camera
    public float maxPosition = 1.3f;

    /// Movement speed of camera
    public float moveSpeed = 2.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(transform.position != target.position)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position,
                targetPos, smoothing);
        }
    }
}
