using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHealthDisplay : HealthDisplay<SpriteRenderer>
{
    [Header("Config")]
    [SerializeField] private string sortingLayerName = "UI";
    [SerializeField] private float gapBetweenContainerCenters = 0.8f;

    protected override void SetComponentWithSprite(SpriteRenderer component, Sprite sprite)
    {
        component.sprite = sprite;
        component.sortingLayerName = sortingLayerName;
    }

    protected override void SetContainerPositionByIndex(GameObject container, int index)
    {
        container.transform.position = new Vector2(index * gapBetweenContainerCenters * transform.localScale.x, 0);
    }
}
