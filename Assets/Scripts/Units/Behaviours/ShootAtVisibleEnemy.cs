using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtVisibleEnemy : TurnBasedBehaviour
{
    [Header("Refs")]
    [SerializeField] Projectile projectilePrefab = null;

    [Header("Config")]
    [SerializeField] float visionRange = 5f;
    [SerializeField] LayerMask enemyLayers = 0;
    [SerializeField] LayerMask visionBlockingLayers = 0;
    [SerializeField] LayerMask projectileBlockingLayers = 0;
    [SerializeField] int attackDamage = 1;

    Projectile projectileShot = null;

    public override IEnumerator TakeTurn()
    {
        DidAction = false;
        projectileShot = null;

        MazeUnit possibleTarget = VisionUtils.FindVisibleEnemy(transform, visionRange, enemyLayers, visionBlockingLayers);

        if (possibleTarget)
        {
            Attack(possibleTarget);
            DidAction = true;

            if (ShouldAnimate())
            {
                animator.SetTrigger("Attack");

                yield return null;

                while (!IsAnimationIdle())
                {
                    yield return null;
                }
            }

            while (projectileShot)
            {
                yield return null;
            }
        }
    }

    private void Attack(MazeUnit target)
    {
        projectileShot = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileShot.SetTarget(target.transform);
        projectileShot.SetObstacleMask(projectileBlockingLayers);
        projectileShot.onHit += OnHit;
    }

    private void OnHit(GameObject other)
    {
        Health health = other.GetComponent<Health>();

        if (health)
        {
            health.TakeDamage(attackDamage);
        }
    }
}
