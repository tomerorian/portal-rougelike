using static MazeGeneration;
using static PopulationUtils;

public class SpawnSkeleton : SimpleUnitSpawnRule
{
    public SpawnSkeleton(int relativeDifficulty, int totalDifficulty) : base(relativeDifficulty, totalDifficulty)
    {
    }

    protected override MazeUnit GetUnitPrefab()
    {
        return PrefabCache.Instance.Skeleton;
    }
}
