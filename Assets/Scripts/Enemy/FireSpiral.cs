using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class FireSpiral : MonoBehaviour
{
    [SerializeField] private EnemyBullet _enemyBullet;
    [SerializeField] private float angleStep = 10.0f;
    
    private float angle = 0.0f;
    private float elapsedTime = 0.0f;

    void Fire()
    {
        Vector3 pos = this.transform.position;
        Quaternion rot = this.transform.rotation;

        float bulletDirX = pos.x + Mathf.Sin((angle * Mathf.PI) / 180.0f);
        float bulletDirY = pos.y + Mathf.Cos((angle * Mathf.PI) / 180.0f);
            
        Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0.0f);
        Vector3 bulletDirection = (bulletMoveVector - pos).normalized;

        EnemyBullet bullet = Instantiate(_enemyBullet, pos, rot);
        bullet.MoveDirection = bulletDirection;

        angle += angleStep;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 0.07f)
        {
            Fire();
            elapsedTime = 0.0f;
        }
    }
}
