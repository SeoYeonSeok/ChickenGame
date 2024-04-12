using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Image fadeImg;
    public float fadeDuration = 1.0f; // 페이드 인/아웃하는 데 걸리는 시간 (초)
    public Color fadeColor = Color.black; // 페이드 인/아웃하는 색상

    private Coroutine fadeCoroutine; // 페이드 Coroutine 참조

    public GameObject mainCam;

    public GameObject chara;
    private string charaModelName;
    public GameObject[] meat;
    public GameObject coin;

    public Transform meatFallingPos;
    public Transform coinFallingPos;

    // 삭제할 것들
    public GameObject[] envs = new GameObject[3];
    public GameObject tileMgr;
    public GameObject[] particles;

    public int remainingCoins;

    public Transform magnetPos;
    public float floatingHeight = 2f; // 공중에 떠 있는 높이
    public float moveSpeed = 10f; // 이동 속도

    public void Activated(int coins)
    {
        // 0.FadeImage를 검은색으로 Fade 시키고, 카메라를 좌표 값으로 이동시키기(0, 35, -70)
        remainingCoins = coins;
        charaModelName = chara.transform.GetChild(0).GetChild(0).name;
        fadeCoroutine = StartCoroutine(FadeImage(true));
    }

    IEnumerator FadeImage(bool fadeIn)
    {
        float timer = 0f;
        Color startColor = fadeIn ? new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f) : fadeColor;
        Color endColor = fadeIn ? fadeColor : new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);

        while (timer < fadeDuration)
        {
            float progress = timer / fadeDuration;
            fadeImg.color = Color.Lerp(startColor, endColor, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        // 1. Camera를 좌표값으로 옮기기     
        Vector3 newPos = new Vector3(0, 35f, -90f);
        mainCam.transform.position = newPos;
        
        Destroy(chara);        
        for (int i = 0; i < envs.Length; i++) Destroy(envs[i]);        
        for (int i = 0; i < particles.Length; i++) Destroy(particles[i]);
               
        // 2. FadeImage를 다시 원래 색으로 만들기
        fadeImg.color = endColor;

        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeDuration;
            fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            yield return new WaitForSeconds(0.01f);
        }

        // 3. 죽은 동물 고기 오브젝트를 MeatFallingPos에서 떨구기        
        GameObject go1 = null;

        if (charaModelName == "Chicken" || charaModelName == "Condor" || charaModelName == "Dragon") 
            go1 = Instantiate(meat[0]);
        else if (charaModelName == "Lion" || charaModelName == "BabyCow" || charaModelName == "Dog" || charaModelName == "Pig" || charaModelName == "Cat" || charaModelName == "Penguin") 
            go1 = Instantiate(meat[1]);

        go1.transform.position = meatFallingPos.position;
    }

    public IEnumerator DropCoins()
    {
        yield return new WaitForSeconds(1f);

        // 5. 지금까지 먹은 코인 개수를 샌 coins 변수를 받아서 CoinFallingPos 안에서 떨구기
        while (remainingCoins > 0)
        {
            yield return new WaitForSeconds(0.1f);
            remainingCoins -= 1;

            float ranX = Random.Range(-0.05f, 0.05f);
            float ranZ = Random.Range(-0.1f, 0.1f);
            Vector3 ranPos = new Vector3(ranX, 0, ranZ);

            GameObject go1 = Instantiate(coin);
            go1.transform.SetParent(coinFallingPos);
            go1.transform.localPosition = ranPos;
            go1.transform.parent = null;

            float ranRotX = Random.Range(0, 360f);
            float ranRotY = Random.Range(0, 360f);
            float ranRotZ = Random.Range(0, 360f);

            go1.transform.localRotation = Quaternion.Euler(ranRotX, ranRotY, ranRotZ);

            StartCoroutine(CoinSpin(go1.GetComponent<RotateItemCorou>()));
            StartCoroutine(Magnet(go1));            
        }
    }

    IEnumerator CoinSpin(RotateItemCorou rotCoin)
    {
        yield return new WaitForSeconds(0.25f);
        rotCoin.isSpin = false;
    }

    IEnumerator Magnet(GameObject coin)
    {
        yield return new WaitForSeconds(4f);

        // Layer 설정으로 자기들끼리 엉키지 않게 하기
        int coinLayer = LayerMask.NameToLayer("Coin");
        coin.layer = coinLayer;

        // 현재 위치에서 공중에 띄우기
        Vector3 startPos = coin.transform.position;
        Vector3 targetPos = startPos + Vector3.up * floatingHeight;
        float elapsedTime = 0f;

        coin.GetComponent<Rigidbody>().useGravity = false;
        coin.GetComponent<Rigidbody>().isKinematic = true;

        while (elapsedTime < 1f)
        {
            coin.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime);
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        coin.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(coin, 2f);

        // 공중의 위치에 도달하면 endPos로 이동
        float forceStrength = 1000f;

        Vector3 direction = magnetPos.transform.position - transform.position;
        float distance = direction.magnitude;
        if (distance > 0.001f) // 게임 오브젝트가 이미 위치에 있는 경우 힘을 적용하지 않음
        {
            Rigidbody rb = coin.GetComponent<Rigidbody>();
            rb.AddForce(direction.normalized * forceStrength);
        }
        else
        {
            Destroy(coin);
        }
    }
}
