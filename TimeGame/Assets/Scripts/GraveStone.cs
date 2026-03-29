using UnityEngine;
using System.Collections;

public class GraveStone : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 moveDirection = Vector3.forward; // direction to move
    public float distance = 5f;                     // distance to move
    public float duration = 2f;                     // time it takes to move

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveDirection.normalized * distance;

        // Start the movement coroutine
        //StartCoroutine(MoveOverTime());
    }
    public void MoveGrave()
    {
        StartCoroutine(MoveOverTime());
    }

    private IEnumerator MoveOverTime()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            // Smooth step for easing in/out
            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Ensure final position is exact
        transform.position = targetPosition;
    }
}
