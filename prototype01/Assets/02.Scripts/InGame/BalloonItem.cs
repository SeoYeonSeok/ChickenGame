using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BalloonItem : MonoBehaviour
{
    public float scaleSpeed1 = 2f; // ũ�⸦ ������Ű�� �ӵ�
    public float scaleSpeed2 = 0.5f; // ũ�⸦ ������Ű�� �ӵ�
    private float targetScale = 5f; // ��ǥ ũ��

    public GameObject powEffect;

    void OnEnable()
    {
        // ó���� ũ�⸦ 0.1�� ����
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // ��ǥ ũ�� ����
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
            // ���� ũ�⸦ ������ ����
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), scaleSpeed1 * Time.deltaTime);
        }        
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetScale, targetScale, targetScale), scaleSpeed2 * Time.deltaTime);
        }
    }

}
