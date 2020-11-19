using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealthDisplay : MonoBehaviour
{
    private const string CONTAINER_NAME = "Hearth Container";

    [Header("Refs")]
    [SerializeField] private Health health = null;
    [SerializeField] private Sprite fullHearthSprite = null;
    [SerializeField] private Sprite halfHearthSprite = null;
    [SerializeField] private Sprite emptyHearthSprite = null;

    [Header("Config")]
    [SerializeField] private float gapBetweenHearthCenters = 16f;

    int displayedMaxHealth = 0;
    int displayedCurrentHealth = 0;

    List<Image> hearthContainers = new List<Image>();

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
                float containerX = transform.position.x + i * gapBetweenHearthCenters * transform.localScale.x;
                newContainer.transform.position = new Vector2(containerX, transform.position.y);
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
            hearthContainers[i].SetNativeSize();
        }

        for (int i = health.GetCurrentHealth(); i < health.GetMaxHealth(); i++)
        {
            hearthContainers[i].sprite = emptyHearthSprite;
            hearthContainers[i].SetNativeSize();
        }
    }

    private GameObject AddContainer()
    {
        GameObject container = new GameObject(CONTAINER_NAME);
        container.transform.parent = transform;
        container.transform.localScale = new Vector3(1, 1, 1);

        Image image = container.AddComponent<Image>();

        hearthContainers.Add(image);

        return container;
    }

    private void RemoveContainer()
    {
        int lastContainerIndex = hearthContainers.Count - 1;
        Image container = hearthContainers[lastContainerIndex];
        hearthContainers.RemoveAt(lastContainerIndex);

        Destroy(container.gameObject);
    }
}
