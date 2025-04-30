using UnityEngine;

public class ResetZoneDetector : MonoBehaviour
{
    public PinManager pin_manager;

    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag == ball)
        pin_manager.OnThrow();

        ///add the ball reset call here too
        
    } 
}
