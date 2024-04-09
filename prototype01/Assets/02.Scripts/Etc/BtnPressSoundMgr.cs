using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPressSoundMgr : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource btnPressSoundMgr;

    public void BtnPressSound()
    {
        btnPressSoundMgr.PlayOneShot(clip);
    }
}
