using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringEffect : MonoBehaviour
{
    public float bounceHeight = 1.0f; // 이동할 높이
    public float bounceSpeed = 1.0f; // 이동 속도
    public float bounceDuration = 0.5f; // 튀는 시간

    private Vector3 initialPosition;
    private bool isShaking = false;
    private float shakeStartTime;

    void Start()
    {
        // 초기 위치 저장
        initialPosition = transform.position;
    }

    void Update()
    {
        // ShakeText() 함수가 호출되면 실행됨
        if (isShaking)
        {
            // 일정 시간 동안 특정 높이로 이동
            float t = (Time.time - shakeStartTime) / bounceDuration;
            if (t <= 1.0f)
            {
                float newY = initialPosition.y + Mathf.Sin(t * Mathf.PI) * bounceHeight;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            else
            {
                // 애니메이션이 끝나면 초기 위치로 복귀하고 애니메이션 종료
                transform.position = initialPosition;
                isShaking = false;
            }
        }
    }

    public void ShakeText()
    {
        // ShakeText() 함수 호출 시 애니메이션 시작
        if (!isShaking)
        {
            // 현재 위치 저장
            initialPosition = transform.position;

            isShaking = true;
            shakeStartTime = Time.time;
        }
    }

}
