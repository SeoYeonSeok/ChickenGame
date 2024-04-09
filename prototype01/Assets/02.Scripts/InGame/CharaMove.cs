using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharaMove : MonoBehaviour
{
    public GameMgr GM;
    public GameOver gameOverScript;
    public Animator camAni;

    public bool isMove = false;
    public bool leftIsTrue_rightIsFalse = false;
    public float moveSpeed = 5f; // 이동 속도

    public int score;
    public int highScore;
    public int coin;
    public int gainCoin;
    public int retry;
    public string pName;
    public bool isFast = false;
    public bool balloon_red = false;
    public bool balloon_blue = false;
    public bool balloon_green = false;
    public GameObject[] balloon_RGB;
    public GameObject[] balloons;
    public int balloonCnt = 0;
    public BalloonFlag balloonFlag;

    public GameObject beforeGamePanel;
    public GameObject onGamePanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scroeTxt;
    public TextMeshProUGUI coinTxt;
    public SpringEffect springEffect;

    public TextMeshProUGUI scoreTextOver;
    public TextMeshProUGUI highScoreTextOver;
    public TextMeshProUGUI newHighScoreTextOver;
    
    public TextMeshProUGUI scoreTextPrefab;
    public Transform canvasTransform;
    public float speed = 1.0f;
    public float duration = 1.0f;

    public GameObject[] env;
    public int envNum = 0;

    public AudioClip[] clips; // 0 이동, 1 코인, 2 추락

    public GameObject charaModel;

    public ParticleSystem boomEffect;
    public ParticleSystem deadEffect;
    public ParticleSystem coinEffect;

    public LightRotateCorou directLight;

    public Image img;
    public float fadeSpeed = 2f;

    public ParticleSystem snow;
    public ParticleSystem rain;
    public GameObject dustTrail; 

    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
        gainCoin = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        retry = PlayerPrefs.GetInt("Retry", 0);
        pName = PlayerPrefs.GetString("PlayerName", "Michael");        

        score = 0;
        scroeTxt.text = score.ToString();

        coinTxt.text = PlayerPrefs.GetInt("Coin").ToString();

        transform.GetComponent<CharaChangeInShop>().InitAll(coin);

        balloons = new GameObject[3];
    }

    void Update()
    {
        if (isMove == false && GM.isGameOver == false) // 게임 시작하기 전에
        {
            if (Input.GetKeyDown(KeyCode.Q)) // 마우스 버튼 클릭을 받으면 게임 시작
            {
                CharaMoveStart();
            }
        }

        if (isMove == true) // 움직이는 동안에
        {
            // 게임 오버 체크
            if (balloon_red == true || balloon_blue == true || balloon_green == true) 
            { }
            else // balloon을 하나라도 얻지 못한 상태라면 
            {
                CheckGameOver(); // 게임 오버
            }

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q)) // 마우스 버튼 클릭을 받으면 이동 방향 전환
            {
                ChangingDirection();
            }

            KeepMoving();
        }

    }

    void CheckGameOver()
    {
        // 캐릭터가 땅바닥에 붙어있는지 감지
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -transform.up, out hit, Mathf.Infinity) && hit.collider.CompareTag("ENV"))
        {
            GameOver();
        }
    }

    void ChangingDirection()
    {        
        // 방향 전환        
        if (leftIsTrue_rightIsFalse)
        {
            Quaternion targetRot = Quaternion.Euler(0, 45, 0);
            charaModel.transform.rotation = targetRot;
        }
        else if (!leftIsTrue_rightIsFalse)
        {
            Quaternion targetRot = Quaternion.Euler(0, -45, 0);
            charaModel.transform.rotation = targetRot;
        }
        
        AddScore(1);

        leftIsTrue_rightIsFalse = !leftIsTrue_rightIsFalse;
        PlayingClip(clips[0]);
    }

    void KeepMoving()
    {
        if (leftIsTrue_rightIsFalse == true) // 좌상단 대각선 이동
        {
            Vector3 mov = new Vector3(-1, 0, 1);
            transform.Translate(mov * moveSpeed * Time.deltaTime);
        }
        else if (leftIsTrue_rightIsFalse == false) // 우상단 대각선 이동
        {
            Vector3 mov = new Vector3(1, 0, 1);
            transform.Translate(mov * moveSpeed * Time.deltaTime);
        }
    }


    void PlayingClip(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    void GameOver()
    {
        PlayingClip(clips[2]);
        
        Instantiate(deadEffect, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);

        // 게임 오버 처리
        GetComponent<Rigidbody>().useGravity = true;

        isMove = false;
        GM.isGameOver = true;

        scroeTxt.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);

        scoreTextOver.text = score.ToString();       

        // 리스트에 저장하는 과정
        RecordList();

        gameOverScript.Activated(gainCoin);
    }

    public void RecordList()
    {
        Record newRecord = new Record(PlayerPrefs.GetString("PlayerName"), score); 
        string curRecordJson = JsonConvert.SerializeObject(newRecord); // 현재 name과 score를 class로 저장한 뒤 json형식으로 직렬화하기

        string filePath = Path.Combine(Application.persistentDataPath, "records.json"); // json파일 불러들이기
        string originJsonFile = File.ReadAllText(filePath); // json 파일 내의 내용물들을 string형 변수로 받기
        

        string newJsonFile = curRecordJson + "," + originJsonFile; // 현재 기록의 json과 기존 json을 ,로 연결하여 합치기       
        if (newJsonFile[newJsonFile.Length - 1] == ',') // newJsonFile의 마지막 글자가 ,이라면 그 ,는 날려버리기
        {
            newJsonFile = newJsonFile.Substring(0, newJsonFile.Length - 1);
        }

        newJsonFile = "[" + newJsonFile + "]"; // 현재 기록 json과 기존 json을 합친것을 []로 감싸기

        List<Record> records = new List<Record>(); // Record List를 생성하기
        records = JsonConvert.DeserializeObject<List<Record>>(newJsonFile); // newJsonFile의 json 문장들을 List 형식으로 역직렬화하기
        records.Sort();

        if (records.Count > 10)
        {
            records.RemoveRange(10, records.Count - 10);
        }

        newJsonFile = JsonConvert.SerializeObject(records); // List를 Json 형식으로 직렬화하기        

        // 겸사겸사 하이스코어 검사도 좀 하기
        if (newRecord.GetScore() > highScore)
        {
            PlayerPrefs.SetInt("HighScore", newRecord.GetScore());
            highScore = PlayerPrefs.GetInt("HighScore");

            newHighScoreTextOver.gameObject.SetActive(true);
        }
        highScoreTextOver.text = highScore.ToString();

        // json 파일에다 적기 전에 []는 벗기기
        if (newJsonFile.Length >= 2 && newJsonFile[0] == '[' && newJsonFile[newJsonFile.Length - 1] == ']') 
        {
            newJsonFile = newJsonFile.Substring(1, newJsonFile.Length - 2);
        }

        // json 파일에다 []를 벗긴 문자열을 저장하기
        File.WriteAllText(filePath, newJsonFile);
    }

    public void SortingListFromJson(string filePath)
    {
        string jSonString = File.ReadAllText(filePath);
        string jSonStringLoad = "[" + jSonString + "]";
        Debug.Log("Loaded JSON String : " + jSonStringLoad);

        List<Record> records = new List<Record>();
        records = JsonConvert.DeserializeObject<List<Record>>(jSonStringLoad);

        records.Sort();

        for (int i = 0; i < records.Count; i++)
        {
            Debug.Log(i + " - " + records[i].GetName() + " : " + records[i].GetScore());
        }
    }

    public void DebugJSON()
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

        SortingListFromJson(filePath);
    }

    public void ClearJson()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "records.json");
        File.WriteAllText(filePath, string.Empty);
    }

    public void CharaMoveStart() // 게임 시작
    {
        StartCharaAnimation();

        retry++;
        PlayerPrefs.SetInt("Retry", retry);

        beforeGamePanel.SetActive(false);
        onGamePanel.SetActive(true);

        AddScore(1);
        
        charaModel = transform.GetChild(0).gameObject;

        Quaternion targetRot = Quaternion.Euler(0, 45, 0);
        charaModel.transform.rotation = targetRot;        

        isMove = true; // 움직이기

        GameObject go = Instantiate(dustTrail);
        go.transform.SetParent(charaModel.transform);
        go.transform.localPosition = new Vector3(0, -0.2f, -0.25f);
        go.transform.localRotation = Quaternion.Euler(0, 90f, 0);
        
        camAni.enabled = false;

        //directLight.StartCoroutine(directLight.RotateItSelf());
    }

    void StartCharaAnimation()
    {
        Animator ani = transform.GetChild(0).GetChild(0).GetComponent<Animator>();

        if (ani != null)
        {
            ani.SetInteger("Walk", 1);
        }       
    }

    public void BoingAnim()
    {
        Animator ani = transform.GetChild(0).GetChild(0).GetComponent<Animator>();

        ani.SetTrigger("Boing");
    }

    void AddScore(int plus)
    {
        score += plus;
        scroeTxt.text = score.ToString();

        springEffect.ShakeText();

        if ((score % 50) == 0 && score != 0) // score가 50의 배수에 도달할 때마다
        {            
            ChangeEnv();
            AddSpeed();
            StartCoroutine(ScreenFade());
        }

        if ((score % 100) == 0 && score != 0) // score가 100의 배수에 도달할 때마다
        {
            // balloon의 num 상승
            if (balloonFlag.GetCnt() < 4) { balloonFlag.UpCnt(); }
        }
    }

    void AddCoin(int plus)
    {
        coin += plus;
        PlayerPrefs.SetInt("Coin", coin); // PlayerPrefs에 저장

        coinTxt.text = PlayerPrefs.GetInt("Coin").ToString();
    }

    void AddSpeed()
    {
        if (moveSpeed <= 7f)
        {
            moveSpeed += 0.1f;
        }        
    }

    void ChangeEnv()
    {
        for (int i = 0; i < env.Length; i++)
        {
            env[i].transform.GetChild(envNum).gameObject.SetActive(false);            
        }

        rain.gameObject.SetActive(false);
        snow.gameObject.SetActive(false);

        envNum++;
        if (envNum > 4) { envNum = 0; }
        
        if (envNum == 1) 
        {
            rain.gameObject.SetActive(true);
            rain.Play(); 
        }
        else if (envNum == 3) 
        {
            snow.gameObject.SetActive(true);
            snow.Play();
        }

        for (int i = 0; i < env.Length; i++)
        {
            env[i].transform.GetChild(envNum).gameObject.SetActive(true);                                 
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item_Coin"))
        {
            Destroy(other.gameObject);

            // 아이템 획득 처리
            AddCoin(1);
            AddScore(1);
            AddScore(1);
            gainCoin++;
            StartCoroutine(ShowScoreText("+2", other.transform.position));
            PlayingClip(clips[1]);

            Instantiate(coinEffect, transform.position, Quaternion.identity);
        }
        else if (other.gameObject.CompareTag("Item_RedB"))
        {
            Destroy(other.gameObject);

            if (balloon_red == false)
            {
                // 아이템 획득 처리
                PlayingClip(clips[1]);
                balloon_red = true;

                StartCoroutine(ShowScoreText("Red Balloon!", other.transform.position));                

                GameObject go = Instantiate(balloon_RGB[0]);
                go.transform.SetParent(charaModel.transform);
                Vector3 newPos = charaModel.transform.localPosition;

                // 일단 뭐든 먹으면
                if (balloons[0] == null) // balloons[]의 0번째 칸이 비어있을 경우
                {
                    newPos.y = 2.3f;
                    go.transform.localPosition = newPos;
                    go.transform.rotation = Quaternion.identity;

                    balloons[0] = go;
                    StartCoroutine(BalloonTimeOut_Red(0));
                }
                else if (balloons[0] != null && balloons[1] == null) // balloons[]의 0번째 칸이 비어있지 않을 경우
                {
                    Vector3 rotation = go.transform.rotation.eulerAngles;
                    rotation.z = -45f;

                    newPos.x = 1.5f;
                    newPos.y = 1.8f;
                    go.transform.localPosition = newPos;
                    go.transform.localRotation = Quaternion.Euler(rotation);

                    balloons[1] = go;
                    StartCoroutine(BalloonTimeOut_Red(1));
                }
                else if (balloons[0] != null && balloons[1] != null) // balloons[]의 0번째, 1번째 칸이 모두 비어있지 않을 경우
                {
                    Vector3 rotation = go.transform.rotation.eulerAngles;
                    rotation.z = 45f;

                    newPos.x = -1.5f;
                    newPos.y = 1.8f;
                    go.transform.localPosition = newPos;
                    go.transform.localRotation = Quaternion.Euler(rotation);

                    balloons[2] = go;
                    StartCoroutine(BalloonTimeOut_Red(2));
                }
            }
            else // Balloon을 먹은 상태에서 한번 더 먹으면 폭발
            {
                GameOver();
                PlayingClip(clips[4]); // 소리
                Instantiate(boomEffect, transform.position, Quaternion.identity); // 이펙트

                Destroy(charaModel);
            }
        }
        else if (other.gameObject.CompareTag("Item_BlueB"))
        {
            Destroy(other.gameObject);

            if (balloon_blue == false)
            {
                // 아이템 획득 처리
                PlayingClip(clips[1]);
                balloon_blue = true;

                StartCoroutine(ShowScoreText("Blue Balloon!", other.transform.position));

                GameObject go = Instantiate(balloon_RGB[1]);
                go.transform.SetParent(charaModel.transform);
                Vector3 newPos = charaModel.transform.localPosition;

                // 일단 뭐든 먹으면
                if (balloons[0] == null) // balloons[]의 0번째 칸이 비어있을 경우
                {
                    newPos.y = 2.3f;
                    go.transform.localPosition = newPos;
                    go.transform.rotation = Quaternion.identity;

                    balloons[0] = go;
                    StartCoroutine(BalloonTimeOut_Blue(0));
                }
                else if (balloons[0] != null && balloons[1] == null) // balloons[]의 0번째 칸이 비어있지 않을 경우
                {
                    Vector3 rotation = go.transform.rotation.eulerAngles;
                    rotation.z = -45f;

                    newPos.x = 1.5f;
                    newPos.y = 1.8f;
                    go.transform.localPosition = newPos;
                    go.transform.localRotation = Quaternion.Euler(rotation);

                    balloons[1] = go;
                    StartCoroutine(BalloonTimeOut_Blue(1));
                }
                else if (balloons[0] != null && balloons[1] != null) // balloons[]의 0번째, 1번째 칸이 모두 비어있지 않을 경우
                {
                    Debug.Log("Final Third");
                    Vector3 rotation = go.transform.rotation.eulerAngles;
                    rotation.z = 45f;

                    newPos.x = -1.5f;
                    newPos.y = 1.8f;
                    go.transform.localPosition = newPos;
                    go.transform.localRotation = Quaternion.Euler(rotation);

                    balloons[2] = go;
                    StartCoroutine(BalloonTimeOut_Blue(2));
                }
            }
            else // Balloon을 먹은 상태에서 한번 더 먹으면 폭발
            {
                GameOver();
                PlayingClip(clips[4]);
                Instantiate(boomEffect, transform.position, Quaternion.identity);

                Destroy(charaModel);
            }
        }
        else if (other.gameObject.CompareTag("Item_GreenB"))
        {
            Destroy(other.gameObject);

            if (balloon_green == false)
            {
                // 아이템 획득 처리
                PlayingClip(clips[2]);
                balloon_green = true;

                StartCoroutine(ShowScoreText("Green Balloon!", other.transform.position));

                GameObject go = Instantiate(balloon_RGB[2]);
                go.transform.SetParent(charaModel.transform);
                Vector3 newPos = charaModel.transform.localPosition;

                // 일단 뭐든 먹으면
                if (balloons[0] == null) // balloons[]의 0번째 칸이 비어있을 경우
                {
                    newPos.y = 2.3f;
                    go.transform.localPosition = newPos;
                    go.transform.rotation = Quaternion.identity;

                    balloons[0] = go;
                    StartCoroutine(BalloonTimeOut_Green(0));
                }
                else if (balloons[0] != null && balloons[1] == null) // balloons[]의 0번째 칸이 비어있지 않을 경우
                {
                    Vector3 rotation = go.transform.rotation.eulerAngles;
                    rotation.z = -45f;

                    newPos.x = 1.5f;
                    newPos.y = 1.8f;
                    go.transform.localPosition = newPos;
                    go.transform.localRotation = Quaternion.Euler(rotation);

                    balloons[1] = go;
                    StartCoroutine(BalloonTimeOut_Green(1));
                }
                else if (balloons[0] != null && balloons[1] != null) // balloons[]의 0번째, 1번째 칸이 모두 비어있지 않을 경우
                {
                    Vector3 rotation = go.transform.rotation.eulerAngles;
                    rotation.z = 45f;

                    newPos.x = -1.5f;
                    newPos.y = 1.8f;
                    go.transform.localPosition = newPos;
                    go.transform.localRotation = Quaternion.Euler(rotation);

                    balloons[2] = go;
                    StartCoroutine(BalloonTimeOut_Green(2));
                }
            }
            else // Balloon을 먹은 상태에서 한번 더 먹으면 폭발
            {
                GameOver();
                PlayingClip(clips[4]);
                Instantiate(boomEffect, transform.position, Quaternion.identity);

                Destroy(charaModel);
            }
        }

        // 화면 바깥의 Boundary에 닿으면
        if (other.gameObject.CompareTag("Boundary"))
        {
            GameOver(); // 게임 오버
        }        
    }


    IEnumerator BalloonTimeOut_Red(int balloonsNum)
    {
        yield return new WaitForSeconds(2f);
        
        balloon_red = false;
        
        if (balloons[balloonsNum] != null)
        {
            Destroy(balloons[balloonsNum]);
            PlayingClip(clips[3]); // 3번 클립 실행
        }
    }

    IEnumerator BalloonTimeOut_Blue(int balloonsNum)
    {
        yield return new WaitForSeconds(2f);

        balloon_blue = false;
        
        if (balloons[balloonsNum] != null)
        {            
            Destroy(balloons[balloonsNum]);
            PlayingClip(clips[3]); // 3번 클립 실행
        }
    }

    IEnumerator BalloonTimeOut_Green(int balloonsNum)
    {
        yield return new WaitForSeconds(2f);

        balloon_green = false;
        
        if (balloons[balloonsNum] != null)
        {
            Destroy(balloons[balloonsNum]);
            PlayingClip(clips[3]); // 3번 클립 실행
        }
    }

    IEnumerator ScreenFade()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            img.color = new Color(Color.gray.r, Color.gray.r, Color.gray.r, alpha);
            yield return new WaitForSeconds(0.01f);
        }

        img.color = new Color(Color.gray.r, Color.gray.r, Color.gray.r, 0);
        yield return null;
    }

    IEnumerator ShowScoreText(string text, Vector3 pos)
    {
        TextMeshProUGUI scoreText = Instantiate(scoreTextPrefab, canvasTransform);
        scoreText.text = text;
        scoreText.transform.position = Camera.main.WorldToScreenPoint(pos);

        float elapsedTime = 0f;
        Vector3 startPos = scoreText.transform.position;

        while (elapsedTime < Time.deltaTime) 
        {
            elapsedTime += Time.deltaTime;
            float newY = Mathf.Lerp(startPos.y, startPos.y + 50f, elapsedTime / duration);
            scoreText.transform.position = new Vector3(startPos.x, newY, startPos.z);
            yield return null;
        }

        Color startColor = scoreText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        elapsedTime = 0f;
        
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime / duration;
            scoreText.color = Color.Lerp(startColor, endColor, elapsedTime);
            yield return null;
        }

        Destroy(scoreText.gameObject);
    }
}
