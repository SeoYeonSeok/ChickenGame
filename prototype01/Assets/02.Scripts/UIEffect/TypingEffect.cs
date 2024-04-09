using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text txt;

    string dialogue;

    void Start()
    {
        txt = GetComponent<TMP_Text>();

        dialogue = txt.text;
        StartCoroutine(Typing(dialogue));
    }

    IEnumerator Typing(string text)
    {
        txt.text = null;

        if (text.Contains("  "))
        {
            text = text.Replace("  ", "\n");
        }            

        for (int i = 0; i < text.Length; i++)
        {
            txt.text += text[i];

            yield return new WaitForSeconds(0.15f);
        }
    }

}
