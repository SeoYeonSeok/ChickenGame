using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateItemCorou : MonoBehaviour
{
    public float rotSpeed = 50f; // 회전 속도    

    private void OnBecameVisible()
    {        
        StartCoroutine(RotateItSelf());
    }

    IEnumerator RotateItSelf()
    {
        while (true) 
        {
            yield return new WaitForSeconds(0.05f);

            transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        }        
    }
}
