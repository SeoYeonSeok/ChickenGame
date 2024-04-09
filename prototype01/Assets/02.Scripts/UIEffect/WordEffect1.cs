using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class WordEffect1 : MonoBehaviour
{
    TMP_Text textMesh;

    Mesh mesh;

    Vector3[] verticles;

    List<int> wordIndexs;
    List<int> wordLenghts;

    public Gradient rainbow;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        verticles = mesh.vertices;

        for (int i = 0; i < verticles.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);

            verticles[i] = verticles[i] + offset;
        }

        mesh.vertices = verticles;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 10f), Mathf.Cos(time * 8f));
    }
}
