using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;
using static PopulationUtils;

public abstract class SimpleItemSpawnRule : SpawnRule
{
    int itemsSpawned = 0;

    protected abstract Item GetItemPrefab();

    protected virtual int GetItemLimit()
    {
        return 0;
    }

    public SimpleItemSpawnRule(int relativeDifficulty, int totalDifficulty) : base(relativeDifficulty, totalDifficulty)
    {
    }

    public override bool AttemptSpawn(MazePopulator.PopulationData data)
    {
        if (GetItemLimit() > 0 && itemsSpawned >= GetItemLimit()) { return false; }

        Cell cell = GetRandomFreeItemCell(data);

        Item item = InstantiateInCell(GetItemPrefab(), cell);
        data.mazeData[cell.x, cell.y].item = item;

        itemsSpawned++;

        return true;
    }
}
