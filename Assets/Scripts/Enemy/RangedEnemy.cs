using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject bulletPrefab;
    public float attackRange = 10f;
    public float bulletSpawnOffset = 1.0f;
    public float shootDelay = 3f;

    private bool canShoot = true;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // Check if the player is within the attack range and can shoot is true
        if (currentState == EnemyState.Detected && canShoot)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
         // if player is in the attack range, start shooting coroutine
            if (distanceToPlayer <= attackRange)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        Vector3 bulletSpawnPosition = transform.position + transform.up * bulletSpawnOffset;
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().SetDirection(directionToPlayer);
        yield return new WaitForSeconds(shootDelay);

        // Allow shooting again after a delay which prevents the enemy to instantiate too many bullets
        canShoot = true;
    }

    protected override void Die()
    {
        base.Die();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
