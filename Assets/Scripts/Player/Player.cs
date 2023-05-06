using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] private float maxHealth = 100.0f;

    public float Health = 100.0f;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    void CheckDeath()
    {
        if (Health <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.layer == (int)BLLayer.BL_ENVIROMENTLASER)
        {
            Health -= 20.0f;
        }
    }
}
