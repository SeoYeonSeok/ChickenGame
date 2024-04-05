using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonColor : MonoBehaviour
{
    public GameObject contents;

    public void AllBtnColorCng()
    {
        foreach (Transform child in contents.transform)
        {
            child.gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    public void SelectBtnColorCng(int id)
    {
        contents.transform.GetChild(id).gameObject.GetComponent<Image>().color = Color.red;      
    }
}
