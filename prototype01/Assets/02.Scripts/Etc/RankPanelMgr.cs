using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class RankPanelMgr : MonoBehaviour
{
    public RectTransform content;
    public GameObject rankPanel;
    List<Record> records;

    private void OnEnable()
    {
        records = new List<Record>();
        SortingList();

        for (int i = 0; i < records.Count; i++)
        {
            GameObject newPanel = Instantiate(rankPanel, content);
            newPanel.transform.parent = content.transform;
            newPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
            newPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = records[i].GetName();
            newPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = records[i].GetScore().ToString();
        }
    }

    public void SortingList()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "records.json");

        if (File.Exists(filePath))
        {
            string js = File.ReadAllText(filePath);
            Debug.Log(js);
        }
        else
        {
            Debug.Log("Can't Find Anything");
        }

        string jSonString = File.ReadAllText(filePath);
        string jSonStringLoad = "[" + jSonString + "]";

        records = JsonConvert.DeserializeObject<List<Record>>(jSonStringLoad);
    }
}
