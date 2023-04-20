using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float minSpeed = 10.0f;
    [SerializeField] private float boundaryThreshold = 0.69f;
    [SerializeField] private float speedIncDecSize = 0.5f;

    [Header("Laser")] 
    [SerializeField] private float laserTimeToAlive = 2.0f;
    [SerializeField] private LineRenderer laserRenderer;
    [SerializeField] private BoxCollider2D laserCollider;

    private float laserElapsedTime = 0.0f;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        laserRenderer.enabled = false;
        laserCollider.enabled = false;
    }

    void ControlSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _player.Speed += speedIncDecSize;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.Speed = _player.Speed - speedIncDecSize < minSpeed ? minSpeed : _player.Speed - speedIncDecSize;
        }
    }
    
    Vector3 GetNewPosition(Vector3 currentPosition)
    {
        if (Input.GetKey(KeyCode.D))
        {
            currentPosition.x += (_player.Speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            currentPosition.x -= (_player.Speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            currentPosition.y += (_player.Speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            currentPosition.y -= (_player.Speed * Time.deltaTime);
        }
        
        ViewportManager.Instance.ClampInViewport(ref currentPosition, boundaryThreshold);

        return currentPosition;
    }

    void FireBallistics()
    {
        float angleStep = 360.0f / 5.0f;
        float angle = 0.0f;
        Vector3 pos = this.transform.position;

        for (int i = 0; i < 5; i++)
        {
            float bulletDirX = pos.x + Mathf.Sin((angle * Mathf.PI) / 180.0f);
            float bulletDirY = pos.y + Mathf.Cos((angle * Mathf.PI) / 180.0f);
            
            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0.0f);
            Vector3 bulletDirection = (bulletMoveVector - pos).normalized;
            
            Quaternion laserRotation = Quaternion.FromToRotation(this.transform.up, bulletDirection);
            
            PlayerBullet ballisticBullet = BulletPool.Instance.GetBallisticBullet();
            if (!ballisticBullet) return;
        
            ballisticBullet.transform.position = pos;
            ballisticBullet.transform.rotation = laserRotation;
            ballisticBullet.MoveDirection = bulletDirection;
            ballisticBullet.gameObject.SetActive(true);

            angle += angleStep;
        }
    }

    void FireLaser()
    {
        laserRenderer.enabled = true;
        laserCollider.enabled = true;
    }

    void CheckActions()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            PlayerBullet bullet = BulletPool.Instance.GetPlayerBullet();
            if (!bullet) return;
            bullet.transform.position = this.transform.position;
            bullet.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            FireBallistics();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            FireLaser();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (laserRenderer.enabled)
        {
            laserElapsedTime += Time.deltaTime;

            if (laserElapsedTime < laserTimeToAlive) return;

            laserRenderer.enabled = false;
            laserCollider.enabled = false;
            laserElapsedTime = 0.0f;
        }
        
        CheckActions();
        ControlSpeed();
        this.transform.position = GetNewPosition(this.transform.position);
    }
}// end of PlayerController
