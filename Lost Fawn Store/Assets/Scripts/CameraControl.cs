using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target; // the player
    public float smoothing;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
