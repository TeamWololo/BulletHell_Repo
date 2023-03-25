using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colGO = col.gameObject;
        
        // Player
        if (colGO.layer == (int)BLLayer.BL_PLAYER)
        {
            colGO.GetComponent<Player>().Health -= 300.0f;
        }

        // PlayerBullet
        if (colGO.layer == (int)BLLayer.BL_PLAYERBULLET || colGO.layer == (int)BLLayer.BL_ENEMYBULLET)
        {
            Destroy(colGO);
        }
    }
}
