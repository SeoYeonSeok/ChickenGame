using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvMoving : MonoBehaviour
{
    public GameObject env;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            float newZpos = env.transform.position.z + 150f;
            env.transform.localPosition = new Vector3(0, 0, newZpos);

            GameObject envObjs = env.transform.GetChild(0).gameObject;

            foreach (Transform child in envObjs.transform) // 모든 자식 객체들의 SetActive를 true로 만들기
            {
                child.gameObject.SetActive(true);
                  
                /*
                if (child.gameObject.GetComponent<EnvObjectItemSpawn>() != null)
                {
                    child.gameObject.GetComponent<EnvObjectItemSpawn>().MakeItem();
                }                
                */
            }
        }
    }
}
