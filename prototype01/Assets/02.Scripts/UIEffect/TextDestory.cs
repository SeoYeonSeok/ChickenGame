using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDestory : MonoBehaviour
{
    public float destroyTime = 0.5f;
    
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
