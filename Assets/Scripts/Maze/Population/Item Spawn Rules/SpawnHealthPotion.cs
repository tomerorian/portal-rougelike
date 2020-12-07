public class SpawnHealthPotion : SimpleItemSpawnRule
{
    public SpawnHealthPotion(int relativeDifficulty, int totalDifficulty) : base (relativeDifficulty, totalDifficulty)
    {
    }

    protected override Item GetItemPrefab()
    {
        return PrefabCache.Instance.HealthPotion;
    }
}
