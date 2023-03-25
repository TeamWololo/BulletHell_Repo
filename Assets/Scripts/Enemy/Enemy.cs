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

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colGO = col.gameObject;
        if (colGO.layer == (int)BLLayer.BL_PLAYERBULLET)
        {
            health -= colGO.GetComponent<PlayerBullet>().damage;
        }
    }
}
