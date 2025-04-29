using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallRespawner : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void RespawnBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = originalRotation;
        rb.position = originalPosition;

        // Optional: Reset forces if you’ve added custom ones
        rb.Sleep(); // Helps ensure it stays still after reset
    }
}
