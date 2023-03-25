using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class FireHemisphere : MonoBehaviour
{
    [SerializeField] private EnemyBullet _enemyBullet;
    [SerializeField] private float damage = 10.0f;

    [Header("Fire Variables")] 
    [SerializeField] private int ammo = 10;
    [SerializeField] private float startAngle = 90.0f;
    [SerializeField] private float endAngle = 270.0f;
    

    private float elapsedTime = 0.0f;
    
    void Fire()
    {
        float angleStep = (endAngle - startAngle) / ammo;
        float angle = startAngle;
        Vector3 pos = this.transform.position;
        Vector3 rotationEuler = this.transform.rotation.eulerAngles;
        Quaternion laserRotation = Quaternion.identity;

        for (int i = 0; i < ammo + 1; i++)
        {
            float bulletDirX = pos.x + Mathf.Sin((angle * Mathf.PI) / 180.0f);
            float bulletDirY = pos.y + Mathf.Cos((angle * Mathf.PI) / 180.0f);
            
            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0.0f);
            Vector3 bulletDirection = (bulletMoveVector - pos).normalized;
            
            rotationEuler.z += angle - 90.0f;
            laserRotation = Quaternion.Euler(rotationEuler);
            
            EnemyBullet bullet = Instantiate(_enemyBullet, pos, laserRotation);
            bullet.MoveDirection = bulletDirection;

            angle += angleStep;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            Fire();
            elapsedTime = 0.0f;
        }
    }
}
