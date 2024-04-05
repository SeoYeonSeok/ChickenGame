using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMgr : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public void ChngShopCoinText(int curCoin)
    {
        coinText.text = curCoin.ToString();
    }
}
