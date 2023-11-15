using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class GraphicExtensions
{
    public static void FadeOut(this Graphic graphicToFadeOut, float fadeDuration, Action callbackAction = null, float pauseOnStart = 0f)
    {
        graphicToFadeOut.StartCoroutine(FadeOutCoroutine(graphicToFadeOut, fadeDuration, callbackAction, pauseOnStart));
    }

    private static IEnumerator FadeOutCoroutine(Graphic graphicToFadeOut, float fadeDuration, Action callbackAction, float pauseOnStart)
    {
        yield return new WaitForSeconds(pauseOnStart);

        float elapsedTime = 0f;
        Color startColor = graphicToFadeOut.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            graphicToFadeOut.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        graphicToFadeOut.gameObject.SetActive(false);
        callbackAction?.Invoke();
    }

    public static void FadeIn(this Graphic graphicToFadeIn, float fadeDuration, Action callbackAction = null, float pauseOnStart = 0f)
    {
        graphicToFadeIn.StartCoroutine(FadeInCoroutine(graphicToFadeIn, fadeDuration, callbackAction, pauseOnStart));
    }

    private static IEnumerator FadeInCoroutine(Graphic graphicToFadeIn, float fadeDuration, Action callbackAction, float pauseOnStart)
    {
        yield return new WaitForSeconds(pauseOnStart);

        graphicToFadeIn.gameObject.SetActive(true);
        float elapsedTime = 0f;
        Color originalColor = graphicToFadeIn.color;
        Color startColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            graphicToFadeIn.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        graphicToFadeIn.color = new Color(endColor.r, endColor.g, endColor.b, 1f);
        callbackAction?.Invoke();
    }

    public static void Pulse(this Graphic graphicToPulse, float pulseSpeed, float pulseAmplitude)
    {
        graphicToPulse.StartCoroutine(PulseCoroutine(graphicToPulse, pulseSpeed, pulseAmplitude));
    }

    private static IEnumerator PulseCoroutine(Graphic graphicToPulse, float pulseSpeed, float pulseAmplitude)
    {
        Vector3 originalScale = graphicToPulse.transform.localScale;
        float sinTime = 0f;

        while (true)
        {
            sinTime += Time.deltaTime;
            float newScale = originalScale.x + Mathf.Sin(sinTime * pulseSpeed) * pulseAmplitude;
            graphicToPulse.transform.localScale = new Vector3(newScale, newScale, 1f);
            yield return null;
        }
    }

    public static void TransparecyOff(this Graphic graphic)
    {
        graphic.gameObject.SetActive(true);
        Color currentColor = graphic.color;
        currentColor.a = 1f;
        graphic.color = currentColor;
    }

    public static void TransparecyOn(this Graphic graphic)
    {
        graphic.gameObject.SetActive(false);
        Color currentColor = graphic.color;
        currentColor.a = 0f;
        graphic.color = currentColor;
    }
}
