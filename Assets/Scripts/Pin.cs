using UnityEngine;

public class Pin : MonoBehaviour
{
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    public bool IsDown()
    {
        return Vector3.Dot(transform.up, Vector3.up) < 0.7f;
    }

    public void ResetPin()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
