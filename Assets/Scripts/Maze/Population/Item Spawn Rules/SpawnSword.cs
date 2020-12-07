public class SpawnSword : SimpleItemSpawnRule
{
    public SpawnSword(int relativeDifficulty, int totalDifficulty) : base (relativeDifficulty, totalDifficulty)
    {
    }

    protected override Item GetItemPrefab()
    {
        return PrefabCache.Instance.Sword;
    }

    protected override int GetItemLimit()
    {
        return 2;
    }
}
