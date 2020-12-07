using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MazeGeneration;
using static PopulationUtils;

public class SpawnSlime : SpawnRule
{
    public SpawnSlime(int relativeDifficulty, int totalDifficulty) : base (relativeDifficulty, totalDifficulty)
    {
    }

    public override bool AttemptSpawn(MazePopulator.PopulationData data)
    {
        Cell cell = GetRandomFreeUnitCell(data);

        MazeUnit enemy = InstantiateInCell(PrefabCache.Instance.Slime, cell);
        data.mazeData[cell.x, cell.y].occupant = enemy;

        return true;
    }
}
