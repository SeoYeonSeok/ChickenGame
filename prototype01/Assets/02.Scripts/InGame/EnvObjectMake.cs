using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvObjectMake : MonoBehaviour
{
    // Env 오브젝트 Prefab
    public GameObject[] envObjPrefab;
    public GameObject[] envObj;

    void Start()
    {
        envObj = new GameObject[20];

        float posY = 1.25f;
        float posZ = -22f;

        for (int i = 0; i < envObj.Length; i++) 
        {
            // 1번째 오브젝트
            int rnd1 = Random.Range(0, envObjPrefab.Length); // 프리팹 중 1개 랜덤하게 받기

            envObj[i] = Instantiate(envObjPrefab[rnd1]);
            envObj[i].transform.SetParent(transform); // 생성 후 Env N의 자식 객체로 두기

            float posX1 = Random.Range(-3.2f, 3.2f); // X축 생성 지점 설정
            
            envObj[i].transform.localPosition = new Vector3(posX1, posY, posZ); // 지점 생성
            envObj[i].transform.gameObject.SetActive(true);

            //envObj[i].transform.gameObject.GetComponent<EnvObjectItemSpawn>().MakeItem();

            // 2번째 오브젝트
            i++;

            rnd1 = Random.Range(0, envObjPrefab.Length); // 프리팹 중 1개 랜덤하게 받기

            envObj[i] = Instantiate(envObjPrefab[rnd1]);
            envObj[i].transform.SetParent(transform); // 생성 후 Env N의 자식 객체로 두기

            float posX2 = 0;
            float posX2Padding = Random.Range(2.5f, 5f);

            if (posX1 >= 0)
            {
                posX2 = posX1 - posX2Padding;
            }
            else
            {
                posX2 = posX1 + posX2Padding;
            }            

            envObj[i].transform.localPosition = new Vector3(posX2, posY, posZ); // 지점 생성
            envObj[i].transform.gameObject.SetActive(true);

            //envObj[i].transform.gameObject.GetComponent<EnvObjectItemSpawn>().MakeItem();

            posZ += 5f; // 10 x 2개씩 만들게 Z축 간격 늘리기
        }
    }
}
