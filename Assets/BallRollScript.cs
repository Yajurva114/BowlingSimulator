using UnityEngine;

public class BallRoll : MonoBehaviour
{
    public float rollForce = 500f;  // You can adjust this in the Inspector

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.forward * rollForce);
            Debug.Log("Ball rolling!");
        }
    }
}