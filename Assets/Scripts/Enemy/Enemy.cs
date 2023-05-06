using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10.0f;
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
    
    void GetInViewport()
    {
        Vector3 position = this.transform.position;
        if (ViewportManager.Instance.IsInsideViewport(position, -1.0f))
        {
            return;
        }

        if (transform.up == -1.0f * Vector3.up)
        {
            transform.position += transform.up * (speed * Time.deltaTime);
        }
        else
        {
            transform.position += transform.up * (-1.0f * speed * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        GetInViewport();
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
