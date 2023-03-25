using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    [SerializeField] private EnemyBullet _enemyBullet;

    private float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Fire()
    {
        Vector3 rotationEuler = this.transform.rotation.eulerAngles;
        Quaternion laserRotation = Quaternion.Euler(rotationEuler.x, rotationEuler.y, rotationEuler.z - 270);
        EnemyBullet enemyBullet = Instantiate(_enemyBullet, this.transform.position, laserRotation);
        enemyBullet.MoveDirection = this.transform.up;
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
