using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonImage : MonoBehaviour
{
    public GameObject contents;

    public Sprite nonSelectBtn;
    public Sprite selectBtn;

    public void AllBtnImageCng()
    {
        foreach (Transform child in contents.transform)
        {
            child.gameObject.GetComponent<Image>().sprite = nonSelectBtn;
        }
    }

    public void SelectBtnImageCng(int id)
    {
        contents.transform.GetChild(id).gameObject.GetComponent<Image>().sprite = selectBtn;
    }
}
