using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject fadeOverlay;
    private Image fadeImage;
    public float fadeDuration;

    void Start()
    {
        fadeImage = fadeOverlay.GetComponent<Image>();
        Assert.IsNotNull(fadeImage);
    }
    
    public void OnPlayButtonClick()
    {
        StartCoroutine(FadeOut());
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    IEnumerator FadeOut()
    {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, timeElapsed / fadeDuration);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);
        
        SceneManager.LoadSceneAsync(1);
    }
}
