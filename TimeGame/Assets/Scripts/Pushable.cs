using TMPro;
using UnityEngine;
using System.Collections;



public class Pushable : MonoBehaviour
{

    [SerializeField] private float moveDistance;
    [SerializeField] private float moveDistancelr;
    [SerializeField] private float moveDuration = 2f;





    [SerializeField] private Vector2 Cords;
    [SerializeField] private Vector2 MaxCords;

    public AudioSource audioSource;

    public bool canPush = true;

    public void Push(Direction pushDirection, PlayerController player)
    {
        print("start");
        if (pushDirection == Direction.Left && Cords.x == 0)
            return;
        if (pushDirection == Direction.Right && Cords.x == MaxCords.x)
            return;
        if (pushDirection == Direction.Down && Cords.y == 0)
            return;
        if (pushDirection == Direction.Up && Cords.y == MaxCords.y)
            return;

        canPush = false;
        var dir = Vector3.zero;

        if (pushDirection == Direction.Left)
        {
            dir = new Vector3(0, 0, -1);
            Cords.x -= 1;
        }
        else if (pushDirection == Direction.Right)
        {
            dir = new Vector3(0, 0, 1);
            Cords.x += 1;
        }
        else if (pushDirection == Direction.Up)
        {
            dir = new Vector3(-1, 0, 0);
            Cords.y += 1;
        }
        else if (pushDirection == Direction.Down)
        {
            dir = new Vector3(1, 0, 0);
            Cords.y -= 1;
        }

        var dist = (pushDirection == Direction.Left || pushDirection == Direction.Right) ? moveDistancelr : moveDistance;

        print("start");
        StartCoroutine(MoveToTarget(player, dir, dist));

        PlayPushSound(audioSource);

    }
    private IEnumerator MoveToTarget(PlayerController player, Vector3 dir, float dist)
    {
        Vector3 startPos = transform.position;
        var targetPosition = transform.position + (dir * dist);

        Vector3 startPosPlayer = player.transform.position;
        var targetPositionPlayer = player.transform.position + (dir * dist);

        float elapsed = 0f;

        player.SetIsPushing(true);
        player.animator.SetTrigger("Push");
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);


            t = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Lerp(startPos, targetPosition, t);
            player.transform.position = Vector3.Lerp(startPosPlayer, targetPositionPlayer, t);
            player.transform.rotation = Quaternion.LookRotation(dir);
            player.SwapCharacterMaterial();
            yield return null;
        }

        transform.position = targetPosition; 
        player.transform.position = targetPositionPlayer;

        StartCoroutine(LerpBack(player, dir, .25f, .25f));




    }

    IEnumerator LerpBack(PlayerController player, Vector3 dir, float distance, float duration)
    {
        Vector3 startPos = player.transform.position;
        Vector3 targetPos = startPos - dir.normalized * distance;

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;

            // Ease in/out (optional but feels WAY better)
            t = Mathf.SmoothStep(0f, 1f, t);

            player.transform.position = Vector3.Lerp(startPos, targetPos, t);

            time += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPos; // ensure exact final pos

        player.SetIsPushing(false);
        player.ResetCharacterMaterial();
        print("move");

        if (Cords.x == 2 && Cords.y == 4)
        {
            FindAnyObjectByType<LadderFall>().MoveLadder();
        }
        canPush = true;
    }

    private void PlayPushSound(AudioSource source)
    {
        if (source == null) return;

        source.pitch = Random.Range(0.95f, 1.05f); 
        source.Play();

    }
}
