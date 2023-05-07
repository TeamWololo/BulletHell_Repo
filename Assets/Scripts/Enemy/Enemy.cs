using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10.0f;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] [Range(0.0f, 100.0f)] private float percentageSpeed = 1.0f;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private float viewportThreshold = 1.0f;

    private float health = 100.0f;

    private void Awake()
    {
        health = maxHealth;
    }

    void CheckDeath()
    {
        if (health <= 0.0f)
        {
            this.gameObject.SetActive(false);
        }

        if (!ViewportManager.Instance.IsInsideViewport(this.transform.position, 10.0f))
        {
            Destroy(this.gameObject);
        }
    }
    
    void GetInViewport()
    {
        Vector3 position = this.transform.position;
        if (ViewportManager.Instance.IsInsideViewport(position, -1.0f * viewportThreshold))
        {
            if (!isBoss)
            {
                transform.position += transform.up * (speed * (percentageSpeed / 100.0f) * Time.deltaTime);
            }
            return;
        }
        
        transform.position += transform.up * (speed * Time.deltaTime);
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
        if (colGO.layer == BLLayers.playerBullet)
        {
            health -= colGO.GetComponent<PlayerBullet>().damage;
            col.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == BLLayers.playerBullet)
        {
            health -= 31.0f;
        }
    }
}
