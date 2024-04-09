using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDeleteWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile") || 
            other.gameObject.CompareTag("Item_Coin") || 
            other.gameObject.CompareTag("Item_RedB") ||
            other.gameObject.CompareTag("Item_BlueB") ||
            other.gameObject.CompareTag("Item_GreenB"))
        {
            Destroy(other.transform.parent.gameObject);
        }   
    }
}
