using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform gun, gunBase, bulletSpawnPos;
    public GameObject bulletPrefab;
    public float bulletSpeed, shootCooldown, bulletLifeTime, maxAngle, minAngle;

    private bool _shoot;

    private void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    public void Rotate(Vector2 direction)
    {
        var angle = Mathf.Atan2(direction.y - gun.position.y, direction.x - gun.position.x) * Mathf.Rad2Deg;
        angle = Mathf.Min(angle, maxAngle);
        angle = Mathf.Max(angle, minAngle);

        gun.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Rotate(float angle)
    {
        angle = Mathf.Min(angle, maxAngle);
        angle = Mathf.Max(angle, minAngle);

        gun.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void StartShooting()
    {
        _shoot = true;
    }

    public void StopShooting()
    {
        _shoot = false;
    }

    public bool isShooting()
    {
        return _shoot;
    }


    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (_shoot)
            {
                var bullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);
                
                if (CompareTag("PlayerCannon"))
                    bullet.layer = LayerMask.NameToLayer("PlayerBullets");
                else if (CompareTag("EnemyCannon"))
                    bullet.layer = LayerMask.NameToLayer("EnemyBullets");
                
                Destroy(bullet, bulletLifeTime);
                var direction = bulletSpawnPos.position - gunBase.position;
                bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(shootCooldown);
        }
    }
}