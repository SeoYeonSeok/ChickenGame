using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnUnlock : MonoBehaviour
{
    public string keyName;
    public GameObject unlockPanel;

    private void Start()
    {
        unlockPanel = transform.GetChild(0).gameObject;
        BtnUnlockThroughKey();
    }

    public void BtnUnlockThroughKey()
    {
        if (PlayerPrefs.GetInt(keyName) == 1)
        {
            Debug.Log("Unlocked " + keyName);
            unlockPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Denying " + keyName);
            unlockPanel.SetActive(true);
        }
    }
}
