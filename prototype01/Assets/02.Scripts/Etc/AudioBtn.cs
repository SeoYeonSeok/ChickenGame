using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioBtn : MonoBehaviour
{
    public AudioSource audioS;
    public Image btnImage;
    public Sprite runnedMusic;
    public Sprite stoppedMusic;
    public bool isPressed = false;

    private void Start()
    {
        audioS = GameObject.FindGameObjectWithTag("AUDIO").GetComponent<AudioSource>();

        if (audioS.mute == true)
        {
            isPressed = true;
            btnImage.sprite = stoppedMusic;
        }
        else
        {
            isPressed = false;
            btnImage.sprite = runnedMusic;
        }
    }

    public void AudioBtnPressed()
    {
        isPressed = !isPressed;
        
        if (isPressed == true)
        {
            audioS.mute = true;
            btnImage.sprite = stoppedMusic;
        }
        else
        {
            audioS.mute = false;
            btnImage.sprite = runnedMusic;
        }
    }
}
