using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMgr : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public AudioClip ddiling;

    public SpringEffect sprEffect;

    public void ChngShopCoinText(int curCoin)
    {
        coinText.text = curCoin.ToString();
    }

    public void ChngShopCoinTextLater(int curCoin)
    {
        StartCoroutine(MinusCoin(curCoin));
    }

    IEnumerator MinusCoin(int curC)
    {
        int cCoin = int.Parse(coinText.text);

        while (cCoin != curC)
        {
            cCoin--;
            coinText.text = cCoin.ToString();
            //GetComponent<AudioSource>().PlayOneShot(ddiling);
            sprEffect.ShakeText();
            yield return new WaitForSeconds(0.002f);
        }

        
    }
}
