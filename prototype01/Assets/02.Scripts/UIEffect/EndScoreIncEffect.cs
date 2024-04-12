using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScoreIncEffect : MonoBehaviour
{
    SpringEffect spr;
    TextMeshProUGUI tmp;
    int originScore;

    public void InitScore()
    {
        spr = GetComponent<SpringEffect>();
        tmp = GetComponent<TextMeshProUGUI>();
        originScore = int.Parse(tmp.text);
        tmp.text = "0";
    }

    public void IncreaseCoroutine()
    {
        StartCoroutine(ScoreIncreaseEffect());
    }

    IEnumerator ScoreIncreaseEffect()
    {        
        // 원래의 점수에 도달할 때까지 반복
        for (int i = 1; i <= originScore; i++)
        {
            tmp.text = i.ToString();
            spr.ShakeText();

            yield return new WaitForSeconds(0.05f);
        }
    }
}
