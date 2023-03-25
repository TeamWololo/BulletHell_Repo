using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class LaserEnemy : MonoBehaviour
{
    [SerializeField] private EnemyBullet _enemyBullet;
    [SerializeField] private float damage = 10.0f;

    private Enemy _enemyBase;

    private float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        _enemyBase = GetComponent<Enemy>();
    }

    void Fire()
    {
        Vector3 rotationEuler = this.transform.rotation.eulerAngles;
        Quaternion laserRotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, rotationEuler.z - 270);
        EnemyBullet enemyBullet = Instantiate(_enemyBullet, this.transform.position, laserRotation);
        enemyBullet.MoveDirection = this.transform.up;
        enemyBullet.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1.5f)
        {
            Fire();
            elapsedTime = 0.0f;
        }
    }
}
