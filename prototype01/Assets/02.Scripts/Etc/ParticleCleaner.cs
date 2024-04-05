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
        // Particle System이 정지되면
        if (!particleSystem.isPlaying)
        {
            // 남아있는 모든 입자를 제거합니다.
            particleSystem.Clear();
        }
    }
}