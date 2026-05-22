namespace WoolGame
{
    public sealed class YarnThreadFactory : EntityFactoryBase<YarnThreadSpawnData, YarnThreadDomain, YarnThreadVisual>
    {
        protected override YarnThreadDomain CreateDomain(YarnThreadSpawnData data)
        {
            return new YarnThreadDomain(data);
        }
    }
}
