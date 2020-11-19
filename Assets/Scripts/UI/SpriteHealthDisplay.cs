using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHealthDisplay : MonoBehaviour
{
    private const string CONTAINER_NAME = "Hearth Container";

    [Header("Refs")]
    [SerializeField] private Health health = null;
    [SerializeField] private Sprite fullHearthSprite = null;
    [SerializeField] private Sprite halfHearthSprite = null;
    [SerializeField] private Sprite emptyHearthSprite = null;

    [Header("Config")]
    [SerializeField] private string sortingLayerName = "UI";
    [SerializeField] private float gapBetweenContainerCenters = 1f;

    int displayedMaxHealth = 0;
    int displayedCurrentHealth = 0;

    List<SpriteRenderer> hearthContainers = new List<SpriteRenderer>();

    private void Update()
    {
        if (health.GetMaxHealth() != displayedMaxHealth || health.GetCurrentHealth() != displayedCurrentHealth)
        {
            UpdateHealthDisplay();
        }
    }

    private void UpdateHealthDisplay()
    {
        UpdateContainers();
        UpdateFill();

        displayedMaxHealth = health.GetMaxHealth();
        displayedCurrentHealth = health.GetCurrentHealth();
    }

    private void UpdateContainers()
    {
        if (displayedMaxHealth < health.GetMaxHealth())
        {
            for (int i = displayedMaxHealth; i < health.GetMaxHealth(); i++)
            {
                GameObject newContainer = AddContainer();
                newContainer.transform.position = new Vector2(i * gapBetweenContainerCenters * transform.localScale.x, transform.position.y);
            }
        }
        else if (displayedMaxHealth > health.GetMaxHealth())
        {
            RemoveContainer();
        }
    }

    private void UpdateFill()
    {
        for (int i = 0; i < health.GetCurrentHealth(); i++)
        {
            hearthContainers[i].sprite = fullHearthSprite;
        }
        
        for (int i = health.GetCurrentHealth(); i < health.GetMaxHealth(); i++)
        {
            hearthContainers[i].sprite = emptyHearthSprite;
        }
    }

    private GameObject AddContainer()
    {
        GameObject container = new GameObject(CONTAINER_NAME);
        container.transform.parent = transform;
        container.transform.localScale = new Vector3(1, 1, 1);

        SpriteRenderer renderer = container.AddComponent<SpriteRenderer>();
        renderer.sortingLayerName = sortingLayerName;

        hearthContainers.Add(renderer);

        return container;
    }

    private void RemoveContainer()
    {
        int lastContainerIndex = hearthContainers.Count - 1;
        SpriteRenderer container = hearthContainers[lastContainerIndex];
        hearthContainers.RemoveAt(lastContainerIndex);

        Destroy(container.gameObject);
    }
}
