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
        int displayedContainers = Mathf.CeilToInt(displayedMaxHealth / 2f);
        int wantedContainers = Mathf.CeilToInt(health.GetMaxHealth() / 2f);

        if (displayedContainers < wantedContainers)
        {
            for (int i = displayedContainers; i < wantedContainers; i++)
            {
                AddContainerAtIndex(i);
            }
        }
        else if (displayedContainers > wantedContainers)
        {
            for (int i = wantedContainers; i < displayedContainers; i++)
            {
                RemoveContainer();
            }
        }
    }

    private void UpdateFill()
    {
        float totalContainers = health.GetMaxHealth() / 2f;
        int fullContainers = Mathf.FloorToInt(health.GetCurrentHealth() / 2f);
        int emptyContainers = Mathf.FloorToInt(Mathf.CeilToInt(totalContainers) - (health.GetCurrentHealth() / 2f));
        int halfContainers = Mathf.CeilToInt(totalContainers - fullContainers - emptyContainers);

        for (int i = 0; i < fullContainers; i++)
        {
            SetComponentWithSprite(hearthContainers[i], fullHearthSprite);
        }

        for (int i = 0; i < halfContainers; i++)
        {
            SetComponentWithSprite(hearthContainers[fullContainers + i], halfHearthSprite);
        }

        for (int i = 0; i < emptyContainers; i++)
        {
            SetComponentWithSprite(hearthContainers[fullContainers + halfContainers + i], emptyHearthSprite);
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
