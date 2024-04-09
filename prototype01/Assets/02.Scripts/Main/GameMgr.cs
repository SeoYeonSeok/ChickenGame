using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    public bool isGameOver = false;
    public GameObject cam;

    public GameObject canv;
    public GameObject beforeGamePanel;
    public GameObject shopPanel;
    public GameObject rulePanel;
    public GameObject langPanel;
    public GameObject profilePanel;
    public GameObject rankPanel;    

    public GameObject[] tilePrefabs;
    public GameObject tileMgr;

    public GameObject[] itemPrefabs;

    // 초기 경로 구축 목적 타일 생성 수
    public int remainingSecondTiles = 20;

    // 마지막으로 생성된 타일
    public GameObject lastTile;

    // 타일 생성 위치 1~7
    public int tileNum;

    // 새로운 닉네임 생성 텍스트
    public TMP_InputField inputfield;

    // 풍선 만들기 여부
    public BalloonFlag balloonFlag;
    public GameObject plusBtnImg;

    // 캐릭터
    public Transform charaPos_shop;
    public Transform charaPos_main;
    public GameObject chara;

    // 애니메이터
    public Animator ani;

    void Start()
    {
        tileNum = 5;

        for (int i = 0; i < remainingSecondTiles; i++)
        {
            TileMake();
        }
    }

    public void BalloonFlagOnOff()
    {
        balloonFlag.SwitchFlag();        
    }

    public void TileMake()
    {
        GameObject tile;

        int rnd = Random.Range(0, 2);

        GameObject tileLine = lastTile.transform.GetChild(5).gameObject;

        if (rnd == 0) // Left Spawn
        {
            tileNum--;

            if (tileNum <= 0) // 좌측으로 넘어 버리면
            {
                tileNum = 2;
                tile = Instantiate(tilePrefabs[1]); // Front Spawn Change
                tile.transform.parent = tileMgr.transform;
                tile.transform.position = lastTile.transform.GetChild(1).transform.position;
                tile.transform.localRotation = Quaternion.identity;
                tile.transform.Rotate(new Vector3(0, -45, 0));
            }
            else
            {
                tile = Instantiate(tilePrefabs[0]);
                tile.transform.parent = tileMgr.transform;
                tile.transform.position = lastTile.transform.GetChild(0).transform.position;
                tile.transform.localRotation = Quaternion.identity;
                tile.transform.Rotate(new Vector3(0, -45, 0));
            }

            DrawTileLine(tileLine, tile.GetComponent<TileID>().ID);

            lastTile = tile;

            ItemMake();
        }
        else if (rnd == 1) // Front Spawn
        {
            tileNum++;
            
            if (tileNum >= 8)
            {
                tileNum = 6;
                tile = Instantiate(tilePrefabs[0]); // Left Spawn Change
                tile.transform.parent = tileMgr.transform;
                tile.transform.position = lastTile.transform.GetChild(0).transform.position;
                tile.transform.localRotation = Quaternion.identity;
                tile.transform.Rotate(new Vector3(0, -45, 0));
            }
            else
            {                
                tile = Instantiate(tilePrefabs[1]);
                tile.transform.parent = tileMgr.transform;
                tile.transform.position = lastTile.transform.GetChild(1).transform.position;
                tile.transform.localRotation = Quaternion.identity;
                tile.transform.Rotate(new Vector3(0, -45, 0));                
            }

            DrawTileLine(tileLine, tile.GetComponent<TileID>().ID);

            lastTile = tile;

            ItemMake();
            //BalloonMake();
        }        
    }

    public void ItemMake()
    {
        int rndLimit = 0;

        if (balloonFlag.GetCnt() == 0) { rndLimit = 70; }
        else if (balloonFlag.GetCnt() == 1) { rndLimit = 80; }
        else if (balloonFlag.GetCnt() == 2) { rndLimit = 90; }
        else if (balloonFlag.GetCnt() == 3) { rndLimit = 100; }

        // Item Spawn Process
        int rnd2 = Random.Range(0, 100);

        if (rnd2 >= 70)
        {
            GameObject item = null;

            int rnd3 = Random.Range(0, rndLimit);

            if (rnd3 >= 0 && rnd3 < 70)
            {
                item = Instantiate(itemPrefabs[0]);
            }
            else if (rnd3 >= 70 && rnd3 < 80)
            {
                item = Instantiate(itemPrefabs[1]);
            }
            else if (rnd3 >= 80 && rnd3 < 90)
            {
                item = Instantiate(itemPrefabs[2]);
            }
            else
            {
                item = Instantiate(itemPrefabs[3]);
            }
            item.transform.position = lastTile.transform.GetChild(2).transform.position;
            item.transform.localRotation = Quaternion.identity;
        }
    }

    public void BalloonMake()
    {
        int rnd = 0;
        float rndPos = Random.Range(1.25f, 2.5f);
        Vector3 balloonPos = new Vector3(0,0,0);
        GameObject go1 = null;

        if (tileNum >= 1 && tileNum < 4)
        {
            balloonPos = new Vector3(rndPos, 0.5f, -rndPos);
        }
        else if (tileNum >= 5 && tileNum < 8)
        {
            balloonPos = new Vector3(-rndPos, 0.5f, rndPos);
        }
        else if (tileNum == 4)
        {
            rnd = Random.Range(0, 2);

            if (rnd == 0)
            {
                balloonPos = new Vector3(-rndPos, 0.5f, rndPos);
            }
            else
            {
                balloonPos = new Vector3(rndPos, 0.5f, -rndPos);
            }
        }

        rnd = Random.Range(0, 4);

        if (rnd == 1)
        {
            go1 = Instantiate(itemPrefabs[1]);
        }
        else if (rnd == 2)
        {
            go1 = Instantiate(itemPrefabs[2]);
        }
        else if (rnd == 3)
        {
            go1 = Instantiate(itemPrefabs[3]);
        }
        
        go1.transform.SetParent(lastTile.transform);
        go1.transform.localRotation = Quaternion.Euler(0, -45f, 0);
        go1.transform.localPosition = balloonPos;
    }

    public void DrawTileLine(GameObject tileL, int tileID)
    {
        if (lastTile.GetComponent<TileID>().ID == 0 && tileID == 0)
        {
            // 좌측으로 직진하는 라인 생성
            tileL.transform.GetChild(0).gameObject.SetActive(true);
            tileL.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (lastTile.GetComponent<TileID>().ID == 0 && tileID == 1)
        {
            // 우측으로 꺾는 라인 생성
            tileL.transform.GetChild(0).gameObject.SetActive(false);
            tileL.transform.GetChild(1).gameObject.SetActive(true);

            lastTile.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
        }
        if (lastTile.GetComponent<TileID>().ID == 1 && tileID == 1)
        {
            // 우측으로 직진하는 라인 생성
            tileL.transform.GetChild(0).gameObject.SetActive(true);
            tileL.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (lastTile.GetComponent<TileID>().ID == 1 && tileID == 0)
        {
            // 좌측으로 꺾는 라인 생성
            tileL.transform.GetChild(0).gameObject.SetActive(false);
            tileL.transform.GetChild(1).gameObject.SetActive(true);

            lastTile.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
        }
    }
    public void PlayerNameSave()
    {
        if (string.IsNullOrEmpty(inputfield.text) == false)
        {
            PlayerPrefs.SetString("PlayerName", inputfield.text);
            
            ReturnMainBtn();
        }
    }

    public void SceneReload()
    {
        SceneManager.LoadScene(0);
    }    

    public void PressBtnAnim(int num)
    {
        beforeGamePanel.gameObject.SetActive(false);

        if (num == 1)
        {
            ani.SetBool("Shop", true);
            shopPanel.gameObject.SetActive(true);
            chara.transform.position = charaPos_shop.position;
            chara.transform.rotation = Quaternion.Euler(0, -135f, 0);
            chara.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }            
        else if (num == 2)
        {
            ani.SetBool("Rule", true);
            rulePanel.gameObject.SetActive(true);
        }
        else if (num == 3)
        {
            ani.SetBool("Profile", true);
            profilePanel.gameObject.SetActive(true);

            inputfield.text = string.Empty;

            inputfield.placeholder.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("PlayerName");
            inputfield.placeholder.gameObject.SetActive(true);
        }                
        else if (num == 4)
        {
            ani.SetBool("Rank", true);
            rankPanel.gameObject.SetActive(true);
        }
        else if (num == 5)
        {
            ani.SetBool("Lang", true);
            langPanel.gameObject.SetActive(true);
        }
    }

    public void PressBtnReturnAnim()
    {
        // 현재 생성된 패널 중 유일하게 SetActive 되어 있는 패널 찾아서 닫기
        GameObject activeChild = null;

        foreach (Transform child in canv.transform)
        {
            if (child.gameObject.activeSelf && child.gameObject.name != "FadeImage")
            {
                activeChild = child.gameObject;
                break;
            }
        }

        // 캐릭터 위치도 초기화
        chara.transform.position = charaPos_main.position;
        chara.transform.rotation = Quaternion.identity;
        chara.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

        activeChild.SetActive(false);
        beforeGamePanel.SetActive(true);

        ani.SetBool("Shop", false);
        ani.SetBool("Rule", false);
        ani.SetBool("Profile", false);
        ani.SetBool("Rank", false);
        ani.SetBool("Lang", false);
    }

    public void PressBtn(float xpos)
    {
        // 움직여야 할 위치
        Vector3 movPos = new Vector3(xpos, cam.transform.position.y, cam.transform.position.z);

        StartCoroutine(MoveCam(movPos));

        // 패널 UI 처리
        beforeGamePanel.gameObject.SetActive(false);

        if (xpos == -35f)
        {
            shopPanel.gameObject.SetActive(true);
            chara.transform.position = charaPos_shop.position;
            chara.transform.rotation = Quaternion.Euler(0, -135f, 0);
            chara.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            rulePanel.gameObject.SetActive(true);
        }
    }

    public void PressBtn2(float xpos)
    {
        // 움직여야 할 위치
        Vector3 movPos = new Vector3(xpos, cam.transform.position.y, -60f);

        StartCoroutine(MoveCam(movPos));

        // 패널 UI 처리
        beforeGamePanel.gameObject.SetActive(false);

        if (xpos == 35f)
        {
            langPanel.gameObject.SetActive(true);
        }
        else if (xpos == -35f)
        {
            profilePanel.gameObject.SetActive(true);

            inputfield.text = string.Empty;

            inputfield.placeholder.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("PlayerName");
            inputfield.placeholder.gameObject.SetActive(true);
        }
        else
        {
            rankPanel.gameObject.SetActive(true);
        }
    }
    

    public void ReturnMainBtn()
    {
        // 움직여야 할 위치
        Vector3 movPos = new Vector3(0f, cam.transform.position.y, -37f);

        StartCoroutine(MoveCam(movPos));

        // 현재 생성된 패널 중 유일하게 SetActive 되어 있는 패널 찾아서 닫기
        GameObject activeChild = null;        

        foreach (Transform child in canv.transform)
        {
            if (child.gameObject.activeSelf && child.gameObject.name != "FadeImage")
            {
                activeChild = child.gameObject;
                break;
            }
        }

        chara.transform.position = charaPos_main.position;        
        chara.transform.rotation = Quaternion.identity;
        chara.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

        activeChild.SetActive(false);
        beforeGamePanel.SetActive(true);
    }

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    IEnumerator MoveCam(Vector3 targetPosition)
    {
        float elapsedTime = 0;
        float movementDuration = 0.5f; // 카메라 이동 속도 (메인 <-> 상점 / ? 화면)

        Vector3 startingPos = cam.transform.position;            

        while (elapsedTime < movementDuration)
        {
            cam.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / movementDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = targetPosition; // 이동 완료 후 보정
    }
}
