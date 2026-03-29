using UnityEngine;
using System.Collections;

public class LadderFall : MonoBehaviour
{
    [Header("Movement Settings")]
    public Vector3 moveDirection = Vector3.forward; // direction to move
    public float distance = 5f;                     // distance to move
    public float duration = 2f;                     // time it takes to move

    private Vector3 startPosition;
    private Vector3 targetPosition;

    public GameObject LadderTp1;
    public GameObject LadderTp2;

    public AudioSource source;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveDirection.normalized * distance;

        // Start the movement coroutine
        //StartCoroutine(MoveOverTime());
    }
    [ContextMenu("Move Ladder")]
    public void MoveLadder()
    {
        StartCoroutine(MoveOverTime());
        LadderTp1.SetActive(true);
        LadderTp2.SetActive(true);
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

    private void PlaySound()
    {
        if (source != null)
        {
            source.Play();
        }
    }
}
