using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] private float maxHealth = 100.0f;

    private float health = 100.0f;

    private void Awake()
    {
        health = maxHealth;
    }

    void CheckDeath()
    {
        if (health <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colGO = col.gameObject;
        if (colGO.layer == (int)BLLayer.BL_PLAYERBULLET)
        {
            health -= colGO.GetComponent<PlayerBullet>().damage;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == (int)BLLayer.BL_PLAYERLASER)
        {
            health -= 31.0f;
        }
    }
}
