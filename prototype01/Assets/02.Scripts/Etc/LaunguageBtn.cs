using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaunguageBtn : MonoBehaviour
{
    public TextMeshProUGUI[] beforeGamePanelTexts;
    public TextMeshProUGUI[] gameOverPanelTexts;
    public TextMeshProUGUI shopPanelTexts;
    public TextMeshProUGUI rulePanelTexts;
    public TextMeshProUGUI langPanelTexts;
    public TextMeshProUGUI[] profilePanelTexts;
    public TextMeshProUGUI rankPanelTexts;

    private void Start()
    {
        int langNum = PlayerPrefs.GetInt("Launguage", 0);

        if (langNum == 0)
        {
            Change2Eng();
        }
        else if (langNum == 1)
        {
            Change2Kor();
        }
    }

    public void Change2Eng()
    {
        PlayerPrefs.SetInt("Launguage", 0);

        // Main
        beforeGamePanelTexts[0].text = "Chicken Game";
        beforeGamePanelTexts[1].text = "Tap to Start";
        beforeGamePanelTexts[2].text = "Retry : " + PlayerPrefs.GetInt("Retry", 0).ToString();
        beforeGamePanelTexts[3].text = "High Score : " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        // InGame - GameOver
        gameOverPanelTexts[0].text = "Game Over";
        gameOverPanelTexts[1].text = "Score";
        gameOverPanelTexts[2].text = "High Score";
        gameOverPanelTexts[3].text = "Restart";

        // Shop
        shopPanelTexts.text = "SHOP";

        // Rule
        rulePanelTexts.text = "blahshsdsldiwdwqdwkediewiwfdmwrfndufmwidiwdmfwrirwf";

        // Lang
        langPanelTexts.text = "Launguage";

        // Profile
        profilePanelTexts[0].text = "Profile Setting";
        profilePanelTexts[1].text = PlayerPrefs.GetString("PlayerName", "Michael");
        profilePanelTexts[2].text = "";
        profilePanelTexts[3].text = "Save";

        // Rank
        rankPanelTexts.text = "Ranking";
    }

    public void Change2Kor()
    {
        PlayerPrefs.SetInt("Launguage", 1);

        // Main
        beforeGamePanelTexts[0].text = "ġŲ ����";
        beforeGamePanelTexts[1].text = "��ġ�ϸ� �����մϴ�";
        beforeGamePanelTexts[2].text = "��õ� Ƚ�� : " + PlayerPrefs.GetInt("Retry", 0).ToString();
        beforeGamePanelTexts[3].text = "�ְ� ��� : " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        // InGame - GameOver
        gameOverPanelTexts[0].text = "���� ����";
        gameOverPanelTexts[1].text = "����";
        gameOverPanelTexts[2].text = "�ְ� ���";
        gameOverPanelTexts[3].text = "�����";

        // Shop
        shopPanelTexts.text = "����";

        // Rule
        rulePanelTexts.text = "����������������������������������������������������������������������";

        // Lang
        langPanelTexts.text = "���";

        // Profile
        profilePanelTexts[0].text = "������ ����";
        profilePanelTexts[1].text = PlayerPrefs.GetString("PlayerName", "Michael");
        profilePanelTexts[2].text = "";
        profilePanelTexts[3].text = "����";

        // Rank
        rankPanelTexts.text = "����";
    }
}
