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
        beforeGamePanelTexts[0].text = "치킨 게임";
        beforeGamePanelTexts[1].text = "터치하면 시작합니다";
        beforeGamePanelTexts[2].text = "재시도 횟수 : " + PlayerPrefs.GetInt("Retry", 0).ToString();
        beforeGamePanelTexts[3].text = "최고 기록 : " + PlayerPrefs.GetInt("HighScore", 0).ToString();

        // InGame - GameOver
        gameOverPanelTexts[0].text = "게임 오버";
        gameOverPanelTexts[1].text = "점수";
        gameOverPanelTexts[2].text = "최고 기록";
        gameOverPanelTexts[3].text = "재시작";

        // Shop
        shopPanelTexts.text = "상점";

        // Rule
        rulePanelTexts.text = "으하하하하으하하하하으하하하하으하하하하으하하하하으하하하하으하하하하";

        // Lang
        langPanelTexts.text = "언어";

        // Profile
        profilePanelTexts[0].text = "프로필 설정";
        profilePanelTexts[1].text = PlayerPrefs.GetString("PlayerName", "Michael");
        profilePanelTexts[2].text = "";
        profilePanelTexts[3].text = "저장";

        // Rank
        rankPanelTexts.text = "순위";
    }
}
