using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotateCorou : MonoBehaviour
{
    public float rotSpeed = 10f; // 회전 속도    

    public IEnumerator RotateItSelf()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);

            if(transform.rotation.y >= 90f)
            {
                transform.Rotate(Vector3.up, (rotSpeed * 10) * Time.deltaTime);
            }
            else
            {
                transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
            }

            
        }
    }
}
