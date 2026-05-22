namespace WoolGame
{
    public sealed class WoolBlockFactory : EntityFactoryBase<WoolBlockSpawnData, WoolBlockDomain, WoolBlockVisual>
    {
        protected override WoolBlockDomain CreateDomain(WoolBlockSpawnData data)
        {
            return new WoolBlockDomain(data);
        }
    }
}
