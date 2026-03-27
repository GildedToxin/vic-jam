using TMPro;
using UnityEngine;
using System.Collections;



public class Pushable : MonoBehaviour
{

    [SerializeField] private Vector3 moveDistance;
    [SerializeField] private float moveDuration = 2f;

    [SerializeField] private bool isPushed;
    [SerializeField] private bool isBiDirectional;


    [SerializeField] private BoxCollider rightBox;
    [SerializeField] private BoxCollider leftBox;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            StartCoroutine(MoveToTarget(other.gameObject.GetComponent<PlayerController>()));
            if (isBiDirectional)
                SwapColliderSide();
            else
                StopColliders();
        }
    }
    private void StopColliders()
    {
        rightBox.gameObject.SetActive(false);
        leftBox.gameObject.SetActive(false);
    }
    private void SwapColliderSide()
    {
        rightBox.gameObject.SetActive(!rightBox.gameObject.activeSelf);
        leftBox.gameObject.SetActive(!leftBox.gameObject.activeSelf);
    }
    private IEnumerator MoveToTarget(PlayerController player)
    {
        Vector3 startPos = transform.position;
        var targetPosition = isBiDirectional && isPushed ? transform.position - moveDistance : transform.position + moveDistance;

        Vector3 startPosPlayer = player.transform.position;
        var targetPositionPlayer = isBiDirectional && isPushed ? player.transform.position - moveDistance : player.transform.position + moveDistance;

        float elapsed = 0f;

        player.SetIsPushing(true);
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);


            t = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(startPos, targetPosition, t);
            player.transform.position = Vector3.Lerp(startPosPlayer, targetPositionPlayer, t);
            yield return null;
        }

        transform.position = targetPosition; 
        player.transform.position = targetPositionPlayer;

        isPushed = !isPushed;
        player.SetIsPushing(false);
    }
}
