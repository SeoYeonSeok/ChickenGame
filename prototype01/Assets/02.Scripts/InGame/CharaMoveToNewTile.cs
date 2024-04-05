using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaMoveToNewTile : MonoBehaviour
{
    public GameMgr gameMgr;

    public SphereCollider charaColl;

    /*
    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Tile"))
        {            
            gameMgr.TileMake();
        }
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile_Trigger"))
        {
            gameMgr.TileMake();
        }
    }

}
