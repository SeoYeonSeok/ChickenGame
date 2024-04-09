using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBtnID : MonoBehaviour
{
    public CharaChangeInShop chara;
    public int ID;
    public int price;

    // 버튼들의 클릭 이벤트
    public void LoadItem()
    {
        chara.ChngChara(ID, price);
    }
}
