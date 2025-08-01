using System;
using System.Collections;
using UnityEngine;

public class SkyDayNight : MonoBehaviour
{
    [SerializeField] private SpriteRenderer skySprite;
    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;

    private float duration = 2.5f;
    private bool isDay = true;
    private Coroutine coroutine;

    private void Start()
    {
        if (skySprite == null)
        {
            skySprite = GetComponent<SpriteRenderer>();
        }

        ChangeColor();
    }

    public void ToggleDayNight()
    {
        isDay = !isDay;
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(SmoothDayAndNight(isDay ? dayColor : nightColor));
    }

    private IEnumerator SmoothDayAndNight(Color targetColor)
    {
        Color startColor = skySprite.color;

        float elasped = 0f;

        while (elasped < duration)
        {
            skySprite.color = Color.Lerp(startColor, targetColor, elasped / duration);
            elasped += Time.deltaTime;
            yield return null;
        }

        skySprite.color = targetColor;
    }

    private void ChangeColor()
    {
        if (skySprite != null)
        {
            skySprite.color = isDay ? dayColor : nightColor;
        }
    }

    public void IsDay(bool value)
    {
        isDay = value;
        ChangeColor();
    }
}
