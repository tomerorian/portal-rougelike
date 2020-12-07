using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;
using static PopulationUtils;

public class SpawnSword : SpawnRule
{
    int swordsSpawned = 0;

    public SpawnSword(int relativeDifficulty, int totalDifficulty) : base (relativeDifficulty, totalDifficulty)
    {
    }

    public override bool AttemptSpawn(MazePopulator.PopulationData data)
    {
        if (swordsSpawned >= 2) { return false; }

        Cell cell = GetRandomFreeItemCell(data);

        Item item = InstantiateInCell(PrefabCache.Instance.Sword, cell);
        data.mazeData[cell.x, cell.y].item = item;

        swordsSpawned++;

        return true;
    }
}
