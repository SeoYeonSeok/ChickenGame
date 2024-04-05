using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCleaner : MonoBehaviour
{
    ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Particle System�� �����Ǹ�
        if (!particleSystem.isPlaying)
        {
            // �����ִ� ��� ���ڸ� �����մϴ�.
            particleSystem.Clear();
        }
    }
}