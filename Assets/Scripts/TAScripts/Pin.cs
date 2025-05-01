using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PinManager : MonoBehaviour
{
    public Transform spawnPointsContainer;
    public GameObject pinPrefab; // Prefab for a single pin
    public Transform[] pinPositions; // Array of initial pin positions
    public List<GameObject> currentPins = new List<GameObject>();
    private List<Transform> spawnPoints = new List<Transform>();
    [Tooltip("Drag your TextMeshPro component here")]
    public TMP_Text scoreText;  // 2️⃣ Expose a TMP_Text reference
    public int currentScore = 0;


    //private int throwCount = 0;

    void Awake() {
        foreach (Transform child in spawnPointsContainer)
            spawnPoints.Add(child);

        scoreText.text = "Score: 0";
    }
    void Start()
    {
        // Initialize the pins at the start of the game
        InitializePins();
    }

    // Call this method after the ball is thrown
    // on throw isn't being called at all. 
    // hook it up to the controller. 
    // keep track of when the bowling ball gets to the end of the alley 
    // add a 5 second delay (?) 
    // yield return new WaitForSeconds(5);



//     public void OnThrow()
// {
//     throwCount++;
//     Debug.Log($"[PinManager] OnThrow called — throwCount is now {throwCount}");

//     if (throwCount == 1)
//     {
//         Debug.Log("[PinManager] First throw: scheduling pin removal in 1s");
//         Invoke(nameof(RemoveKnockedDownPins), 2f);
//     }
//     else if (throwCount == 2)
//     {
//         Debug.Log("[PinManager] Second throw: scheduling full reset in 1s");
//         Invoke(nameof(ResetPins), 1f);
//         throwCount = 0;
//     }
// }

    // Removes pins that have fallen over
    // issue could be function may not be called. 
    // look at the rotation of the pin in the axis 
    [ContextMenu("Remove Pings")]
    public void RemoveKnockedDownPins()
    {
        Debug.Log("[PinManager] RemoveKnockedDownPins start — checking each pin’s rotation");

        List<GameObject> standingPins = new List<GameObject>();
        int firstRoundScore = 0;
        foreach (GameObject pin in currentPins)
        {
            if (pin == null) 
                continue;

            // read raw eulerAngles (0–360)
            float xDeg = pin.transform.rotation.eulerAngles.x;
            float zDeg = pin.transform.rotation.eulerAngles.z;

            // remap to -180…+180
            if (xDeg > 180f) xDeg -= 360f;
            if (zDeg > 180f) zDeg -= 360f;

            Debug.Log(
                $"[PinManager] Pin '{pin.name}' rotation X: {xDeg:F1}°, Z: {zDeg:F1}°"
            );

            // if either axis exceeds 70°, it's fallen
            if (Math.Abs(xDeg) < 70f || Math.Abs(zDeg) < 70f)
            {
                Debug.LogWarning(
                    $"[PinManager] Destroying '{pin.name}' — tipped over!"
                );
                pin.SetActive(false);

                Debug.Log("Adding one point");
                firstRoundScore += 1;
            }
            else
            {
                standingPins.Add(pin);
            }
        }
        
        SetScore(firstRoundScore);
        currentPins = standingPins;
        Debug.Log(
            $"[PinManager] RemoveKnockedDownPins complete — {currentPins.Count} pins remain"
        );
    }
   

[ContextMenu("reset Pings")]
    // Resets pins to their initial positions
    public void ResetPins()
    {

        int secondRoundScore = 0;
        //Destroy existing pins
        foreach (GameObject pin in currentPins)
        {
            if (pin == null) 
                continue;

            // read raw eulerAngles (0–360)
            float xDeg = pin.transform.rotation.eulerAngles.x;
            float zDeg = pin.transform.rotation.eulerAngles.z;

            // remap to -180…+180
            if (xDeg > 180f) xDeg -= 360f;
            if (zDeg > 180f) zDeg -= 360f;

            // if either axis exceeds 70°, it's fallen
            if (Math.Abs(xDeg) < 70f || Math.Abs(zDeg) < 70f)
            {
                //pin.SetActive(false);
                secondRoundScore += 1;
            }
        }

        SetScore(secondRoundScore);

        InitializePins();

    }
    public void InitializePins() {
         // destroy any stray pins (if you need this)
        foreach (var pin in currentPins) if (pin) Destroy(pin);
        currentPins.Clear();

        // spawn fresh pins
        foreach (var marker in spawnPoints)
        {
            if (marker == null || pinPrefab == null) continue;
            var newPin = Instantiate(pinPrefab, marker.position, marker.rotation);
            currentPins.Add(newPin);
        }
    }
    public void SetScore(int newScore)
    {
        currentScore = currentScore + newScore;
        scoreText.text = $"Score: {currentScore}";
    }
}
