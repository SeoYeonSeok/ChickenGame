using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI txt;
    public float fadeDuration = 0.5f;
    public float alphaValue = 100f;
    private bool isFadingIn = true;



    void OnEnable()
    {
        if (txt == null) txt = transform.GetComponent<TextMeshProUGUI>();

        StartCoroutine(FadingText());
    }

    IEnumerator FadingText()
    {
        while (true)
        {
            // isFadingIn이 true인 경우, 텍스트를 서서히 불투명하게 만듭니다.
            if (isFadingIn)
            {
                while (txt.color.a < 1)
                {
                    Color color = txt.color;
                    color.a += Time.deltaTime / fadeDuration;
                    txt.color = color;
                    yield return null;
                }
                isFadingIn = false;
            }
            else // isFadingIn이 false인 경우, 텍스트를 서서히 투명하게 만듭니다.
            {
                while (txt.color.a > 0)
                {
                    Color color = txt.color;
                    color.a -= Time.deltaTime / fadeDuration;
                    txt.color = color;
                    yield return null;
                }
                isFadingIn = true;
            }
        }
    }
}
