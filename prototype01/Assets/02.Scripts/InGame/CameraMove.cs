using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameMgr GM;

    public GameObject targetChara;
    public Vector3 targetCharaPos;

    void Start()
    {
        targetCharaPos = transform.position;
    }

    void Update()
    {
        if (GM.isGameOver == false && targetChara.GetComponent<CharaMove>().isMove == true)
        {
            targetCharaPos.z = targetChara.transform.localPosition.z - 8f;

            transform.position = targetCharaPos;
        }
    }
}
