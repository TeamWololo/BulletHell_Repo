using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 150.0f;
    
    public float damage = 10.0f;
    public Vector3 MoveDirection = Vector3.up;

    private float leftEdgeX = 0.0f;
    private float rightEdgeX = 0.0f;
    private float upEdgeY = 0.0f;
    private float bottomEdgeY = 0.0f;

    void Start()
    {
        if (!Camera.main)
        {
            Debug.Log("Cannot found main camera");
            return;
        }

        Camera mainCamera = Camera.main;
        
        leftEdgeX  = mainCamera.ViewportToWorldPoint(Vector3.zero).x - 1.0f;
        rightEdgeX = mainCamera.ViewportToWorldPoint(Vector3.right).x + 1.0f;
        upEdgeY = mainCamera.ViewportToWorldPoint(Vector3.up).y + 1.0f;
        bottomEdgeY = mainCamera.ViewportToWorldPoint(Vector3.zero).y - 1.0f;
    }

    void CheckOutside()
    {
        Vector3 position = this.transform.position;
        if (position.x <= leftEdgeX ||
            position.x >= rightEdgeX ||
            position.y <= bottomEdgeY ||
            position.y >= upEdgeY)
        {
            Destroy(this.gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position += MoveDirection * (speed * Time.deltaTime);
        CheckOutside();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }
}
