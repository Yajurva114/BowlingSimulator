using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BallReturnZone : MonoBehaviour
{
    [Tooltip("Your PinManager instance")]
    public PinManager pinManager;

    [Tooltip("First ball's BallRespawner")]
    public BallRespawner ballRespawner1;

    [Tooltip("Second ball's BallRespawner")]
    public BallRespawner ballRespawner2;

    private int passCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        // only start the coroutine once per pass
        if (!other.CompareTag("Ball")) return;
        StartCoroutine(HandleBallPass());
    }

    private IEnumerator HandleBallPass()
    {
        passCount++;

        if (passCount == 1)
            Debug.Log("[BallReturnZone] First pass – scheduling pin removal in 3s");
        else if (passCount == 2)
            Debug.Log("[BallReturnZone] Second pass – scheduling full rack reset in 3s");

        // wait so the pins can settle or the player can see the gutter
        yield return new WaitForSeconds(3f);

        // pin logic
        if (passCount == 1)
            pinManager.RemoveKnockedDownPins();
        else if (passCount == 2)
        {
            pinManager.ResetPins();
            passCount = 0;
        }

        // respawn *both* balls
        if (ballRespawner1 != null)
        {
            Debug.Log("[BallReturnZone] Respawning ball 1");
            ballRespawner1.RespawnBall();
        }
        if (ballRespawner2 != null)
        {
            Debug.Log("[BallReturnZone] Respawning ball 2");
            ballRespawner2.RespawnBall();
        }
    }
}
