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
        component.SetNativeSize();
    }

    protected override void SetContainerPositionByIndex(GameObject container, int index)
    {
        float containerX = transform.position.x + index * gapBetweenHearthCenters * transform.localScale.x;
        container.transform.position = new Vector2(containerX, transform.position.y);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.black;
        UnityEditor.Handles.Label(transform.position, "Health Display", style);
    }
#endif
}
