using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaChangeInShop : MonoBehaviour
{
    public int itemID;

    // 캐릭터 모델 (캐릭터의 자식)
    public GameObject chara;

    // 캐릭터 모델의 프리팹들
    public GameObject[] charaPrefabs;

    // 캐릭터 모델을 변경할 권한에 대한 Key 값들
    public int sample0Key;
    public int sample1Key;
    public int sample2Key;
    public int sample3Key;
    public int sample4Key;
    public int sample5Key;
    public int sample6Key;
    public int sample7Key;
    public int sample8Key;
    public int sample9Key;

    //public ShopButtonColor shopBtns;
    public ShopButtonImage shopBtns;
    public ShopMgr shopMgr;


    public void InitAll(int currentCoin)
    {
        chara = transform.GetChild(0).gameObject; // 자식 객체 초기화

        shopMgr.ChngShopCoinText(currentCoin); // 상점 화면의 코인 텍스트 초기화

        InitKey(); // 키 초기화
        itemID = PlayerPrefs.GetInt("ItemID", 0); // 현재 ItemID의 키 값 받아오기
        ChngCharaStart(itemID); // 시작과 동시에 ItemID의 키 값에 해당하는 캐릭터 프리팹 변경 함수 실행
    }

    private void InitKey()
    {
        sample0Key = PlayerPrefs.GetInt("S0Key", 1); // 기본 아이템은 Key를 1로 무조건 초기화
        sample1Key = PlayerPrefs.GetInt("S1Key", 0);
        sample2Key = PlayerPrefs.GetInt("S2Key", 0);
        sample3Key = PlayerPrefs.GetInt("S3Key", 0);
        sample4Key = PlayerPrefs.GetInt("S4Key", 0);
        sample5Key = PlayerPrefs.GetInt("S5Key", 0);
        sample6Key = PlayerPrefs.GetInt("S6Key", 0);
        sample7Key = PlayerPrefs.GetInt("S7Key", 0);
        sample8Key = PlayerPrefs.GetInt("S8Key", 0);
        sample9Key = PlayerPrefs.GetInt("S9Key", 0);
    }

    
    public void ChngCharaStart(int itemID)
    {
        if (ReturnKey(itemID) == true) // 키가 있는지 없는지 검사, 있으면 (구매한 상태면) 아래 코드 실행
        {
            ChngPrefabs(charaPrefabs[itemID]);
            PlayerPrefs.SetInt("ItemID", itemID);
            //shopBtns.AllBtnColorCng();
            shopBtns.AllBtnImageCng();
            //shopBtns.SelectBtnColorCng(PlayerPrefs.GetInt("ItemID"));
            shopBtns.SelectBtnImageCng(PlayerPrefs.GetInt("ItemID"));
        }
    }

    public void ChngChara(int itemID, int price)
    {
        if (ReturnKey(itemID) == true) // 키가 있는지 없는지 검사, 있으면 (구매한 상태면) 아래 코드 실행
        {
            ChngPrefabs(charaPrefabs[itemID]);
            PlayerPrefs.SetInt("ItemID", itemID);
            //shopBtns.AllBtnColorCng();
            shopBtns.AllBtnImageCng();
            //shopBtns.SelectBtnColorCng(PlayerPrefs.GetInt("ItemID"));
            shopBtns.SelectBtnImageCng(PlayerPrefs.GetInt("ItemID"));
        }
        else // Key가 열리지 않은 상태라면 코인과 가격 검사 함수 실행
        {
            UnlockKey(price, itemID);
        }
    }

    public void ChngPrefabs(GameObject prefab)
    {
        Destroy(chara.transform.GetChild(0).gameObject); // 자식 객체 파괴하고

        GameObject newChild = Instantiate(prefab, transform);
        newChild.transform.SetParent(chara.transform);
    }
    
    
    private bool ReturnKey(int id)
    {
        if (id == 0)
        {            
            if (PlayerPrefs.GetInt("S0Key") == 1) return true;
            else return false;
        }
        else if (id == 1)
        {
            if (PlayerPrefs.GetInt("S1Key") == 1) return true;
            else return false;
        }
        else if (id == 2)
        {
            if (PlayerPrefs.GetInt("S2Key") == 1) return true;
            else return false;
        }
        else if (id == 3) 
        {
            if (PlayerPrefs.GetInt("S3Key") == 1) return true;
            else return false;
        }
        else if (id == 4)
        {
            if (PlayerPrefs.GetInt("S4Key") == 1) return true;
            else return false;
        }
        else if (id == 5)
        {
            if (PlayerPrefs.GetInt("S5Key") == 1) return true;
            else return false;
        }
        else if (id == 6)
        {
            if (PlayerPrefs.GetInt("S6Key") == 1) return true;
            else return false;
        }
        else if (id == 7)
        {
            if (PlayerPrefs.GetInt("S7Key") == 1) return true;
            else return false;
        }
        else if (id == 8)
        {
            if (PlayerPrefs.GetInt("S8Key") == 1) return true;
            else return false;
        }
        else if (id == 9)
        {
            if (PlayerPrefs.GetInt("S9Key") == 1) return true;
            else return false;
        }

        return false;
    }

    private void UnlockKey(int price, int id)
    {
        CharaMove chara = transform.GetComponent<CharaMove>();
        int curCoin = PlayerPrefs.GetInt("Coin");

        if (curCoin >= price) // 현재 CharaMove 컴포넌트 내의 coin과 가격(100) 비교
        {
            curCoin -= price;

            //shopMgr.ChngShopCoinText(curCoin);
            shopMgr.ChngShopCoinTextLater(curCoin);
            chara.coinTxt.text = curCoin.ToString();

            PlayerPrefs.SetInt("Coin", curCoin);

            if (id == 1) PlayerPrefs.SetInt("S1Key", 1);
            else if (id == 2) PlayerPrefs.SetInt("S2Key", 1);
            else if (id == 3) PlayerPrefs.SetInt("S3Key", 1);
            else if (id == 4) PlayerPrefs.SetInt("S4Key", 1);
            else if (id == 5) PlayerPrefs.SetInt("S5Key", 1);
            else if (id == 6) PlayerPrefs.SetInt("S6Key", 1);
            else if (id == 7) PlayerPrefs.SetInt("S7Key", 1);
            else if (id == 8) PlayerPrefs.SetInt("S8Key", 1);
            else if (id == 9) PlayerPrefs.SetInt("S9Key", 1);
        }
    }

    public void DebugKey()
    {
        PlayerPrefs.SetInt("S1Key", 0);
        PlayerPrefs.SetInt("S2Key", 0);
        PlayerPrefs.SetInt("S3Key", 0);
        PlayerPrefs.SetInt("S4Key", 0);
        PlayerPrefs.SetInt("S5Key", 0);
        PlayerPrefs.SetInt("S6Key", 0);
        PlayerPrefs.SetInt("S7Key", 0);
        PlayerPrefs.SetInt("S8Key", 0);
        PlayerPrefs.SetInt("S9Key", 0);
    }

    public void DebugKey2()
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 100);

        CharaMove charaM = transform.GetComponent<CharaMove>();
        charaM.coinTxt.text = PlayerPrefs.GetInt("Coin").ToString();

        shopMgr.ChngShopCoinText(PlayerPrefs.GetInt("Coin"));
    }
}
