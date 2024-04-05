using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BalloonItem : MonoBehaviour
{
    public float scaleSpeed1 = 2f; // 크기를 증가시키는 속도
    public float scaleSpeed2 = 0.5f; // 크기를 증가시키는 속도
    private float targetScale = 5f; // 목표 크기

    public GameObject powEffect;

    void OnEnable()
    {
        // 처음에 크기를 0.1로 설정
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // 목표 크기 설정
        targetScale = 1.0f;
    }

    private void OnDisable()
    {
        GameObject go = Instantiate(powEffect, transform.position, Quaternion.identity);
        go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void Update()
    {
        if (transform.localScale.x <= 2f)
        {
            // 현재 크기를 서서히 증가
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), scaleSpeed1 * Time.deltaTime);
        }        
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), scaleSpeed2 * Time.deltaTime);
        }
    }

}
