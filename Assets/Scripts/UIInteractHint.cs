using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(CanvasGroup))]
public class UIInteractHint : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    public bool isActive;
    public TextMeshProUGUI tmp;
    private const float fadeDuration = 0.2f;
    private void Start()
    {
         canvasGroup = GetComponent<CanvasGroup>();
         Assert.IsNotNull(canvasGroup);
    }

    public void Activate()
    {
        StartCoroutine(FadeIn());
    }

    public void Deactivate()
    {
        StartCoroutine(FadeOut());
    }

    public void SetText(string text)
    {
        tmp.text = text;
    }

    public IEnumerator FadeIn()
    {
        isActive = true;
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0;

        while (canvasGroup.alpha < 1 && isActive)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(0f, 1f, startAlpha + timeElapsed / fadeDuration);
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
            canvasGroup.alpha = Mathf.SmoothStep(1f, 0f, 1 - startAlpha + timeElapsed / fadeDuration);
            yield return null;
        }
        
        if (!isActive) canvasGroup.alpha = 0;
    }
}
