using UnityEngine;
using System.Collections;

public class WakeUpScreen : MonoBehaviour
{
    // This script handles the black screen at the start of the game, and it fading

    [SerializeField] CanvasGroup overlay;
    [SerializeField] float holdOverlay = 1f;
    [SerializeField] float fadeDuration = 4f;

    [SerializeField] AnimationCurve opacity = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f); // An animation curve for the opacity to mimic blinking (1=black,0=transparent)

    [SerializeField] DialogueSO introDialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Run());
    }    
    
    void Awake()
    {
        overlay.alpha = 1f;
    }

    public IEnumerator Play()
    {
        overlay.alpha = 1f;
        yield return new WaitForSeconds(holdOverlay);

        for (float t = 0f; t < 1f; t += Time.deltaTime / fadeDuration)
        {
            overlay.alpha = opacity.Evaluate(t);
            yield return null;
        }

        overlay.alpha = 0f;
    }

    IEnumerator Run()
    {
        yield return Play();
        if (introDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(introDialogue);
        }
    }
}
