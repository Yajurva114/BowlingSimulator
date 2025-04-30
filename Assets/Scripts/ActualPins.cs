using UnityEngine;
using System;

public class ActualPins : MonoBehaviour
{
    private Vector3 position;
    private Quaternion rotation;
    private GameObject pin;
    public bool hasFallen { get; private set; } = false ;

    void Start()
    {
        pin = gameObject;
        position = pin.transform.position;
        rotation = pin.transform.rotation;
    }
    
    private void FixedUpdate()
    {
        if (!hasFallen && (Math.Abs(pin.transform.rotation.eulerAngles.z) > 70 || Math.Abs(pin.transform.rotation.eulerAngles.x) > 70))
        {
                hasFallen = true;
                Debug.Log(pin.name+" has fallen");
        }
    }

    public void Hide()
    {
        pin.SetActive(false);
    }

    public void Reset()
    {
        var pinRigidbody = pin.GetComponent<Rigidbody>();
        pin.transform.position = position;
        pin.transform.rotation = rotation;
        pinRigidbody.Sleep();
        gameObject.SetActive(true);
        pinRigidbody.WakeUp();
        hasFallen = false;
    }

    public void ResetBooleanFallen()
    {
        hasFallen = false;
    }
}
