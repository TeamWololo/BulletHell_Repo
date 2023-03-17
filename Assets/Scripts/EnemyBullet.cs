using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 100.0f;
    
    public float damage = 10.0f;
    public Vector3 MoveDirection = Vector3.down;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += MoveDirection * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }
}
