using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    /// Target of the camera
    public Transform target;
    public Camera cam;

    /// Minimum position of camera
    public float maxY, minY, maxX, minX;
    public float minPosition = -1.3f;

    /// Maximum position of camera
    public float maxPosition = 1.3f;

    /// Movement speed of camera
    public float moveSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }
        Vector2 world2Camera = cam.WorldToViewportPoint(target.transform.position);
        if (minX < world2Camera.x  && world2Camera.x  < maxX && minY < world2Camera.y && world2Camera.y < maxY)
        {
            return;
        }

        var newPosition = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, minPosition, maxPosition);
        newPosition.y = transform.position.y;
        newPosition.z = transform.position.z;

        transform.position = newPosition;
    }
}
