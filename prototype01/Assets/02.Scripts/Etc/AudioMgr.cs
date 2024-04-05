using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    private static AudioMgr instance;

    public AudioSource bgmSource;

    public AudioClip[] clips;

    private void Awake()
    {
        // AudioManager 인스턴스를 설정하고, 씬이 전환되어도 파괴되지 않도록 합니다.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // AudioSource 설정을 업데이트하는 메서드
    public void UpdateAudioSettings(float volume, AudioClip clip)
    {
        bgmSource.volume = volume;
        bgmSource.clip = clip;
        // 필요한 다른 설정도 여기에 추가할 수 있습니다.
    }    
}
