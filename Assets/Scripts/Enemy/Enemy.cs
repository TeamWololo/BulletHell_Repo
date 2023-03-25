using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] private float maxHealth = 100.0f;

    private float health = 100.0f;

    private void Awake()
    {
        health = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject colGO = col.gameObject;
        if (colGO.layer == 8)
        {
            health -= colGO.GetComponent<PlayerBullet>().damage;
        }
    }
}
