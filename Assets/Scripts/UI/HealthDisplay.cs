using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthDisplay<T>: MonoBehaviour where T : Component
{
    private const string CONTAINER_NAME = "Hearth Container";

    [Header("Refs")]
    [SerializeField] private Health health = null;
    [SerializeField] private Sprite fullHearthSprite = null;
    [SerializeField] private Sprite halfHearthSprite = null;
    [SerializeField] private Sprite emptyHearthSprite = null;

    int displayedMaxHealth = 0;
    int displayedCurrentHealth = 0;

    List<T> hearthContainers = new List<T>();

    protected abstract void SetComponentWithSprite(T component, Sprite sprite);
    protected abstract void SetContainerPositionByIndex(GameObject container, int index);

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
                AddContainerAtIndex(i);
            }
        }
        else if (displayedMaxHealth > health.GetMaxHealth())
        {
            RemoveContainer();
        }
    }

    private void UpdateFill()
    {
        for (int i = 0; i < health.GetCurrentHealth() && i < health.GetMaxHealth(); i++)
        {
            SetComponentWithSprite(hearthContainers[i], fullHearthSprite);
        }

        for (int i = health.GetCurrentHealth(); i < health.GetMaxHealth(); i++)
        {
            SetComponentWithSprite(hearthContainers[i], emptyHearthSprite);
        }
    }

    private void AddContainerAtIndex(int index)
    {
        GameObject container = new GameObject(CONTAINER_NAME);
        container.transform.parent = transform;
        container.transform.localScale = new Vector3(1, 1, 1);

        T component = container.AddComponent<T>();

        hearthContainers.Add(component);

        SetContainerPositionByIndex(container, index);
    }

    private void RemoveContainer()
    {
        int lastContainerIndex = hearthContainers.Count - 1;
        T container = hearthContainers[lastContainerIndex];
        hearthContainers.RemoveAt(lastContainerIndex);

        Destroy(container.gameObject);
    }
}
