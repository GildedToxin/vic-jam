using UnityEngine;
using System.Collections;

public class Ladder : Interactable
{
    [SerializeField] private Vector3 moveDistance;
    [SerializeField] private float moveDuration = 2f;

    [SerializeField] private Vector3 moveDistanceSide;
    [SerializeField] private float moveDurationSide = 2f;


    public bool atTopOfLadder = false;

    public Transform ladderStartPos;


    [SerializeField] private bool canClimbUpLadder = true;
    public override void Interact(PlayerController player)
    {
        if (!canClimbUpLadder)
            return;


            StartCoroutine(MoveUpLadder(player));

    }

    public IEnumerator MoveUpLadder(PlayerController player)
    {
        player.transform.position = ladderStartPos.position;
        player.transform.rotation = ladderStartPos.rotation;

        Vector3 startPosPlayer = player.transform.position;
        var targetPositionPlayer = player.transform.position + moveDistance;

        float elapsed = 0f;

        player.SetIsPushing(true);
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);


            t = Mathf.SmoothStep(0f, 1f, t);

            player.transform.position = Vector3.Lerp(startPosPlayer, targetPositionPlayer, t);
            yield return null;
        }

        player.transform.position = targetPositionPlayer;

     


        startPosPlayer = player.transform.position;
        targetPositionPlayer = player.transform.position + moveDistanceSide;

       elapsed = 0f;

       // player.SetIsPushing(true);
        while (elapsed < moveDurationSide)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDurationSide);


            t = Mathf.SmoothStep(0f, 1f, t);

            player.transform.position = Vector3.Lerp(startPosPlayer, targetPositionPlayer, t);
            yield return null;
        }

        player.transform.position = targetPositionPlayer;

        player.SetIsPushing(false);
    }
    public IEnumerator MoveDownLadder(PlayerController player)
    {
        return null;
    }

    public void SetCanClimbTower(bool canClimb)
    {
        canClimbUpLadder = canClimb;
    }
}
