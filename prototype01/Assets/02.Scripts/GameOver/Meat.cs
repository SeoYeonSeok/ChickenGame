using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    public ParticleSystem landingSmokeEffect;
    public Rigidbody rb;

    public GameOver gameOver;

    void Start()
    {
        smokeEffect = transform.GetChild(0).GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();

        gameOver = GameObject.FindWithTag("GM").GetComponent<GameOver>();
    }

    private void OnCollisionEnter(Collision coll)
    {
        rb.useGravity = false;
        rb.isKinematic = true;

        smokeEffect.Play();        

        StartCoroutine(gameOver.DropCoins());
    }
}
