using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvObject : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            gameObject.SetActive(false);
        }
    }
}
