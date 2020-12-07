using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;
using static PopulationUtils;

public class SpawnHealthPotion : SpawnRule
{
    public SpawnHealthPotion(int relativeDifficulty, int totalDifficulty) : base (relativeDifficulty, totalDifficulty)
    {
    }

    public override bool AttemptSpawn(MazePopulator.PopulationData data)
    {
        Cell cell = GetRandomFreeItemCell(data);

        Item item = InstantiateInCell(PrefabCache.Instance.HealthPotion, cell);
        data.mazeData[cell.x, cell.y].item = item;

        return true;
    }
}
