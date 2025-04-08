using UnityEngine;
using UnityEngine.XR;

public class XRRecenter : MonoBehaviour
{
    void Start()
    {
        Invoke("Recenter", 0.1f);  // Give time for tracking to initialize
    }

    void Recenter()
    {
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
}
