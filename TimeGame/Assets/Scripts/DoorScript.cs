using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    public bool LeftDoor = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void RotateDoor()
    {
        StartCoroutine(RotateY45(transform, 1f));
    }
    IEnumerator RotateY45(Transform obj, float duration)
    {
        Quaternion startRot = obj.rotation;
        Quaternion targetRot = LeftDoor ? startRot * Quaternion.Euler(0f, 70f, 0f) : startRot * Quaternion.Euler(0f, -70f, 0f);

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            obj.rotation = Quaternion.Slerp(startRot, targetRot, t);

            time += Time.deltaTime;
            yield return null;
        }

        obj.rotation = targetRot;

      Application.Quit();
    }
}
