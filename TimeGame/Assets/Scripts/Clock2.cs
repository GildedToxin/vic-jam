using UnityEngine;

public class Clock2 : MonoBehaviour
{
    public Transform minuteHand;
    public Transform hourHand;

    [Tooltip("How fast time passes (1 = real-time, 60 = 1 min per second, etc)")]
    public float timeScale = 60f;

    private float time; // in minutes

    void Update()
    {
        // Advance time (in minutes)
        time += Time.deltaTime * timeScale;

        // Minute hand: 360° per 60 minutes
        float minuteAngle = (time % 60f) * 6f; // 360 / 60 = 6

        // Hour hand: 360° per 720 minutes (12 hours)
        float hourAngle = (time % 720f) * 0.5f; // 360 / 720 = 0.5

        // Apply rotation (adjust axis if needed)
        minuteHand.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
        hourHand.localRotation = Quaternion.Euler(0, 0, -hourAngle);
    }
}
