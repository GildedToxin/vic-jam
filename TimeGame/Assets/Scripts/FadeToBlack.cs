using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public Image panel; // assign your black panel
    public float duration = 1f;

    public GameObject e;
    public GameObject r;
    void Awake()
    {
        panel = GetComponentInChildren<Image>();
    }
    public void FadeOut()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0f;
        Color color = panel.color;

        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            color.a = Mathf.Lerp(start, end, t);
            panel.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        color.a = end;
        panel.color = color;
        e.SetActive(true);
        r.SetActive(true);
    }

    public void Quit()
    {

        Application.Quit();
    }
}