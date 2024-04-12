using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCtrl : MonoBehaviour
{
    private void OnBecameVisible()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
