using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonFlag : MonoBehaviour
{
    public int balloonCnt = 0;
    public bool flag = false;


    public void UpCnt()
    {        
        balloonCnt++;
    }

    public int GetCnt()
    {
        return balloonCnt;
    }

    public void SwitchFlag()
    {
        flag = !flag;
    }

    public bool IsFlagged()
    {
        return flag;
    }
}
