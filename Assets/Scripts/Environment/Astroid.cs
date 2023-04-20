using UnityEngine;

public class Astroid : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colGO = col.gameObject;
        
        // Player
        if (colGO.layer == (int)BLLayer.BL_PLAYER)
        {
            colGO.GetComponent<Player>().Health -= 300.0f;
        }

        // PlayerBullet && EnemyBullet
        if (colGO.layer == (int)BLLayer.BL_PLAYERBULLET || colGO.layer == (int)BLLayer.BL_ENEMYBULLET)
        {
            colGO.SetActive(false);
        }
    }
}
