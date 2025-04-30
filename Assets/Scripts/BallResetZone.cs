using UnityEngine;

public class BallResetZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Tooltip("Drag your PinManager GameObject here")]
    public PinManager pinManager;

    private void OnTriggerEnter(Collider other)
    {
        // only react to your bowling ball
        if (other.CompareTag("Ball"))
        {
            Debug.Log("[PinTriggerZone] Ball entered – removing knocked-down pins");
            pinManager.RemoveKnockedDownPins();
        }
    }
}
