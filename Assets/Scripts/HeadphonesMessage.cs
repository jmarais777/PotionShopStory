using UnityEngine;
using System.Collections;

public class HeadphonesMessage : MonoBehaviour
{
    // This script handles the headphones message played before the main menu

    static bool HasPlayed;

    [SerializeField] CanvasGroup overlay;
    [SerializeField] float holdOverlay = 1f;
    [SerializeField] float fadeDuration = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        if (HasPlayed)
        {
            overlay.alpha = 0f;
            return;
        }

        HasPlayed = true;
        StartCoroutine(Play());
    }    

    IEnumerator Play()
    {
        overlay.alpha = 1f;
        yield return new WaitForSeconds(holdOverlay);

        for (float t = 0f; t < 1f; t += Time.deltaTime / fadeDuration)
        {
            overlay.alpha = 1f -t;
            yield return null;
        }

        overlay.alpha = 0f;
    }
}
