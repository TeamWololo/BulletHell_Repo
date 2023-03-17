using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 150.0f;
    
    public float damage = 10.0f;
    public Vector3 MoveDirection = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += MoveDirection * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }
}
