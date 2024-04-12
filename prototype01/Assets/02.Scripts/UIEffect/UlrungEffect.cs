using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UlrungEffect : MonoBehaviour
{
    public float shake = 50f;
    public TMP_Text txtComp;
    public float alpha;


    private void Start()
    {
        StartCoroutine(TxtShakedAndFade());        
    }

    IEnumerator TxtShakedAndFade()
    {
        txtComp.color = new Color(txtComp.color.r, txtComp.color.g, txtComp.color.b, 0f);

        while (true)
        {
            yield return new WaitForSeconds(0.025f);

            if (alpha < 1) alpha += Time.deltaTime;

            txtComp.color = new Color(txtComp.color.r, txtComp.color.g, txtComp.color.b, alpha);

            txtComp.ForceMeshUpdate();

            var txtInfo = txtComp.textInfo;

            for (int i = 0; i < txtInfo.characterCount; i++)
            {
                var charInfo = txtInfo.characterInfo[i];

                if (!charInfo.isVisible)
                {
                    continue;
                }

                var verts = txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

                for (int j = 0; j < 4; j++)
                {
                    var orig = verts[charInfo.vertexIndex + j];
                    verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * shake, 0);
                }
            }

            for (int i = 0; i < txtInfo.meshInfo.Length; i++)
            {
                var meshInfo = txtInfo.meshInfo[i];
                meshInfo.mesh.vertices = meshInfo.vertices;
                txtComp.UpdateGeometry(meshInfo.mesh, i);
            }
        }
    }

    /*
    private void Update()
    {
        if (dur < 1) dur += Time.deltaTime / 2;

        txtComp.color = new Color(txtComp.color.r, txtComp.color.g, txtComp.color.b, dur);

        txtComp.ForceMeshUpdate();

        var txtInfo = txtComp.textInfo;

        for (int i = 0; i < txtInfo.characterCount; i++)
        {
            var charInfo = txtInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            var verts = txtInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j = 0; j < 4; j++)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * shake, 0);
            }
        }

        for (int i = 0; i < txtInfo.meshInfo.Length; i++)
        {
            var meshInfo = txtInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            txtComp.UpdateGeometry(meshInfo.mesh, i);
        }
    }
    */
}
