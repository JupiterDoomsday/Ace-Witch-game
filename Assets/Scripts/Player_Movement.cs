using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{ 
    public float speed= .25f;
    Vector3 targetPos;
    Vector3 startPos;
    public bool moving;
    void Update()
    {
        if (moving) {
            if (Vector2.Distance(startPos, transform.position) > 1f) {
                transform.position = targetPos;
                moving = false;
                return;
            }
            transform.position+= (targetPos-startPos) * speed * Time.deltaTime;
            return;
        }
       
       if (Input.GetAxisRaw("Horizontal") == 1f)
        {
            targetPos = transform.position += Vector3.right;
            startPos = transform.position;
            moving = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1f)
        {
            targetPos = transform.position += Vector3.left;
            startPos = transform.position;
            moving = true;
        }
        else if (Input.GetAxisRaw("Vertical") == 1f)
        {
            targetPos = transform.position += Vector3.up;
            startPos = transform.position;
            moving = true;
        }
        else if (Input.GetAxisRaw("Vertical") == -1f)
        {
            targetPos = transform.position += Vector3.down;
            startPos = transform.position;
            moving = true;
        }
    }
}

