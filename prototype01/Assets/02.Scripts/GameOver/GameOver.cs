using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Image fadeImg;
    public float fadeDuration = 1.0f; // ���̵� ��/�ƿ��ϴ� �� �ɸ��� �ð� (��)
    public Color fadeColor = Color.black; // ���̵� ��/�ƿ��ϴ� ����

    private Coroutine fadeCoroutine; // ���̵� Coroutine ����

    public GameObject mainCam;

    public GameObject meat;
    public GameObject coin;

    public Transform meatFallingPos;
    public Transform coinFallingPos;

    public GameObject tileMgr;

    public int remainingCoins;

    public Transform magnetPos;
    public float floatingHeight = 2f; // ���߿� �� �ִ� ����
    public float moveSpeed = 10f; // �̵� �ӵ�

    public void Activated(int coins)
    {
        // 0.FadeImage�� ���������� Fade ��Ű��, ī�޶� ��ǥ ������ �̵���Ű��(0, 35, -70)
        fadeCoroutine = StartCoroutine(FadeImage(true));
        remainingCoins = coins;
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

        // 1. Camera�� ��ǥ������ �ű��
        Vector3 newPos = new Vector3(0, 35f, -90f);
        mainCam.transform.position = newPos;

        Destroy(tileMgr);


        // 2. FadeImage�� �ٽ� ���� ������ �����
        fadeImg.color = endColor;

        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeDuration;
            fadeImg.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
            yield return new WaitForSeconds(0.01f);
        }        

        // 3. ���� ���� ��� ������Ʈ�� MeatFallingPos���� ������
        GameObject go1 = Instantiate(meat);
        go1.transform.position = meatFallingPos.position;
    }

    public IEnumerator DropCoins()
    {
        yield return new WaitForSeconds(1f);

        // 5. ���ݱ��� ���� ���� ������ �� coins ������ �޾Ƽ� CoinFallingPos �ȿ��� ������
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

        // ���� ��ġ���� ���߿� ����
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

        // ������ ��ġ�� �����ϸ� endPos�� �̵�
        float forceStrength = 1000f;

        Vector3 direction = magnetPos.transform.position - transform.position;
        float distance = direction.magnitude;
        if (distance > 0.001f) // ���� ������Ʈ�� �̹� ��ġ�� �ִ� ��� ���� �������� ����
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
