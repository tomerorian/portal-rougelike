using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazePopulator;

public abstract class SpawnRule
{
    int relativeDifficulty = 0;
    int totalDifficulty = 0;

    public SpawnRule(int relativeDifficulty, int totalDifficulty)
    {
        this.relativeDifficulty = relativeDifficulty;
        this.totalDifficulty = totalDifficulty;
    }

    public int GetRelativeDifficulty()
    {
        return relativeDifficulty;
    }

    public int GetTotalDifficulty()
    {
        return totalDifficulty;
    }

    public abstract bool AttemptSpawn(PopulationData data);
}
