using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] Vector2 originalOrientation = Vector2.right;
    [SerializeField] float speed = 8f;

    Transform target = null;

    public void SetTarget(Transform target)
    {
        this.target = target;
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Vector2 lineToTarget = new Vector2(target.position.x - transform.position.x, transform.position.y - target.position.y);
        float angle = Vector2.SignedAngle(lineToTarget, originalOrientation);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        // DEBUG
        if (!target)
        {
            target = GameSession.Instance.player.transform;
        }
        else
        {
            LookAtTarget();
        }
        // DEBUG

        if (!target) { return; }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
