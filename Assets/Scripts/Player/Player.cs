using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    [SerializeField] private float maxHealth = 100.0f;

    public HealthBar healthBar;

    public float Health = 100.0f;

    private Animator animator;
    private static readonly int Explosion = Animator.StringToHash("expl");

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
            StartExplosion();
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (healthBar)
        {
            healthBar.SetMaxHealth(maxHealth);
        }

        animator = GetComponent<Animator>();
    }

    void StartExplosion()
    {
        if (!animator)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetBool(Explosion, true);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject go = other.gameObject;
        if (go.layer == BLLayers.environmentLaser)
        {
            Health -= 30.0f;
        }
    }
}
