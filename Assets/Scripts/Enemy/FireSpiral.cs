using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class FireSpiral : MonoBehaviour
{
    [SerializeField] private float angleStep = 10.0f;
    [SerializeField] private float damage = 15.0f;
    [SerializeField] private float firerateForEveryCircle = 0.07f;
    
    private float angle = 0.0f;
    private float elapsedTime = 0.0f;
    
    private Enemy _enemyBase;
    
    void Start()
    {
        _enemyBase = GetComponent<Enemy>();
    }

    void Fire()
    {
        if (_enemyBase.isDead)
        {
            return;
        }
        
        Vector3 pos = this.transform.position;
        Quaternion rot = this.transform.rotation;

        float bulletDirX = pos.x + Mathf.Sin((angle * Mathf.PI) / 180.0f);
        float bulletDirY = pos.y + Mathf.Cos((angle * Mathf.PI) / 180.0f);
            
        Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0.0f);
        Vector3 bulletDirection = (bulletMoveVector - pos).normalized;

        EnemyBullet enemyBullet = BulletPool.Instance.GetEnemyCircleBullet();
        if (!enemyBullet) return;
        
        enemyBullet.transform.position = pos;
        enemyBullet.transform.rotation = rot;
        enemyBullet.MoveDirection = bulletDirection;
        enemyBullet.damage = damage;
        enemyBullet.gameObject.SetActive(true);

        angle += angleStep;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > firerateForEveryCircle)
        {
            Fire();
            elapsedTime = 0.0f;
        }
    }
}
