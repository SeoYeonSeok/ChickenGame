using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringEffect : MonoBehaviour
{
    public float bounceHeight = 1.0f; // �̵��� ����
    public float bounceSpeed = 1.0f; // �̵� �ӵ�
    public float bounceDuration = 0.5f; // Ƣ�� �ð�

    private Vector3 initialPosition;
    private bool isShaking = false;
    private float shakeStartTime;

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPosition = transform.position;
    }

    void Update()
    {
        // ShakeText() �Լ��� ȣ��Ǹ� �����
        if (isShaking)
        {
            // ���� �ð� ���� Ư�� ���̷� �̵�
            float t = (Time.time - shakeStartTime) / bounceDuration;
            if (t <= 1.0f)
            {
                float newY = initialPosition.y + Mathf.Sin(t * Mathf.PI) * bounceHeight;
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
            else
            {
                // �ִϸ��̼��� ������ �ʱ� ��ġ�� �����ϰ� �ִϸ��̼� ����
                transform.position = initialPosition;
                isShaking = false;
            }
        }
    }

    public void ShakeText()
    {
        // ShakeText() �Լ� ȣ�� �� �ִϸ��̼� ����
        if (!isShaking)
        {
            // ���� ��ġ ����
            initialPosition = transform.position;

            isShaking = true;
            shakeStartTime = Time.time;
        }
    }

}
