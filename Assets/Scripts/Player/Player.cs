using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] private float maxHealth = 100.0f;

    public HealthBar healthBar;

    public float Health = 100.0f;

    private void Awake()
    {
        Health = maxHealth;
    }

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
        if (healthBar)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();

        if (healthBar)
        {
            healthBar.SetHealth(Health);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.layer == BLLayers.environmentLaser)
        {
            Health -= 20.0f;
        }
    }
}
