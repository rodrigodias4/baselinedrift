using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIInteractHint : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public bool isActive;
    private const float fadeDuration = 0.2f;
    private void Start()
    {
         canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Activate()
    {
        StartCoroutine(FadeIn());
    }

    public void Deactivate()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        isActive = true;
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0;

        while (canvasGroup.alpha < 1 && isActive)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(startAlpha, 1f, timeElapsed / fadeDuration);
            yield return null;
        }

        if (isActive) canvasGroup.alpha = 1;
    }

    public IEnumerator FadeOut()
    {
        isActive = false;
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0;

        while (canvasGroup.alpha > 0 && !isActive)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(startAlpha, 0f, timeElapsed / fadeDuration);
            yield return null;
        }
        
        if (!isActive) canvasGroup.alpha = 0;
    }
}
