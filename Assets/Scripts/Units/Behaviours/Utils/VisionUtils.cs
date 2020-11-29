using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VisionUtils
{
    public static MazeUnit FindVisibleEnemy(Transform origin, float visionRange, LayerMask enemyLayers, LayerMask visionBlockingLayers)
    {
        // Find all enemies within range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin.position, visionRange, Vector2.zero, 0, enemyLayers);

        if (hits.Length == 0) { return null; }

        // Find first visible target
        foreach (RaycastHit2D hit in hits)
        {
            RaycastHit2D visibleHit = Physics2D.Raycast(origin.position, hit.transform.position - origin.position, float.MaxValue, enemyLayers | visionBlockingLayers);

            if (hit.collider == visibleHit.collider)
            {
                return hit.collider.GetComponent<MazeUnit>();
            }
        }

        return null;
    }
}
