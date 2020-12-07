using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;
using static PopulationUtils;

public class SpawnSlime : SimpleUnitSpawnRule
{
    public SpawnSlime(int relativeDifficulty, int totalDifficulty) : base (relativeDifficulty, totalDifficulty)
    {
    }

    protected override MazeUnit GetUnitPrefab()
    {
        return PrefabCache.Instance.Slime;
    }
}
