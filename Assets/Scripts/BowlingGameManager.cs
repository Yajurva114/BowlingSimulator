using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BowlingGameManager : MonoBehaviour
{
    public List<Pin> pins; // Drag pins here in Inspector
    private int rollInFrame = 1;

    void Start()
    {
        foreach (var pin in pins)
        {
            pin.originalPosition = pin.transform.position;
            pin.originalRotation = pin.transform.rotation;
        }
    }

    public void OnBallSettled()
    {
        StartCoroutine(HandlePinsAfterDelay());
    }

    IEnumerator HandlePinsAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        int downed = pins.Count(p => p.IsDown());

        if (rollInFrame == 1)
        {
            if (downed == 10)
            {
                ResetAllPins();
                rollInFrame = 1;
            }
            else
            {
                RemoveFallenPins();
                rollInFrame++;
            }
        }
        else
        {
            ResetAllPins();
            rollInFrame = 1;
        }
    }

    void RemoveFallenPins()
    {
        foreach (var pin in pins)
        {
            if (pin.IsDown())
            {
                pin.gameObject.SetActive(false);
            }
        }
    }

    void ResetAllPins()
    {
        foreach (var pin in pins)
        {
            pin.gameObject.SetActive(true);
            pin.ResetPin();
        }
    }

    // TEMP for testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) // Press S to simulate ball settling
        {
            OnBallSettled();
        }
    }
}


