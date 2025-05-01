using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class BallReturnZone : MonoBehaviour
{
    [Tooltip("Your PinManager instance")]
    public PinManager pinManager;

    private int passCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        // start the delayed handling, passing the ball's collider
        StartCoroutine(HandleBallPass(other));
    }

    private IEnumerator HandleBallPass(Collider ballCollider)
    {
        passCount++;

        bool first = passCount == 1;
        bool second = passCount == 2;

        Debug.Log(first
            ? "[BallReturnZone] First pass – scheduling pin removal in 3s"
            : "[BallReturnZone] Second pass – scheduling full rack reset in 3s");

        // **hide** the ball visually & turn off its collider so it "disappears"
        var ballGO = ballCollider.gameObject;
        var renderer = ballGO.GetComponent<Renderer>();
        var collider = ballCollider;
        if (renderer != null) renderer.enabled = false;
        collider.enabled = false;

        // let physics settle and give the player feedback
        yield return new WaitForSeconds(3f);

        // do pins logic
        if (first)
            pinManager.RemoveKnockedDownPins();
        else if (second)
        {
            pinManager.ResetPins();
            passCount = 0;
        }

        // **respawn** the ball
        var respawner = ballGO.GetComponent<BallRespawner>();
        if (respawner != null)
            respawner.RespawnBall();
        else
            Debug.LogWarning("[BallReturnZone] No BallRespawner on ball!");

        // **un-hide** the ball
        if (renderer != null) renderer.enabled = true;
        collider.enabled = true;
    }
}
