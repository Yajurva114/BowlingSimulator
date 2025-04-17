using UnityEngine;

public class PinPhysicsActivator : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Unfreeze X/Z so it can fall naturally
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
