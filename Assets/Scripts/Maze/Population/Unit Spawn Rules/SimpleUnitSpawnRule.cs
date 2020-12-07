using static MazeGeneration;
using static PopulationUtils;

public abstract class SimpleUnitSpawnRule : SpawnRule
{
    protected abstract MazeUnit GetUnitPrefab();

    public SimpleUnitSpawnRule(int relativeDifficulty, int totalDifficulty) : base(relativeDifficulty, totalDifficulty)
    {
    }

    public override bool AttemptSpawn(MazePopulator.PopulationData data)
    {
        Cell cell = GetRandomFreeUnitCell(data);

        MazeUnit enemy = InstantiateInCell(GetUnitPrefab(), cell);
        data.mazeData[cell.x, cell.y].occupant = enemy;

        return true;
    }
}
