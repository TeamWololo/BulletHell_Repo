using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float speed = 150.0f;
    
    public float damage = 10.0f;
    public Vector3 MoveDirection = Vector3.up;

    void Start()
    {

    }

    void CheckOutside()
    {
        Vector3 position = this.transform.position;
        if (!ViewportManager.Instance.IsInsideViewport(position, 1.0f))
        {
            this.gameObject.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        this.transform.position += MoveDirection * (speed * Time.deltaTime);
        CheckOutside();
    }
}
