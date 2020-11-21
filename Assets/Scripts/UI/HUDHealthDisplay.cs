using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealthDisplay : HealthDisplay<Image>
{
    [Header("Config")]
    [SerializeField] private float gapBetweenHearthCenters = 16f;

    protected override void SetComponentWithSprite(Image component, Sprite sprite)
    {
        component.sprite = sprite;
    }

    protected override void SetContainerPositionByIndex(GameObject container, int index)
    {
    }
}
