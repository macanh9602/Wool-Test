namespace WoolGame
{
    public sealed class ConveyorFactory : EntityFactoryBase<ConveyorSpawnData, ConveyorDomain, ConveyorVisual>
    {
        protected override ConveyorDomain CreateDomain(ConveyorSpawnData data)
        {
            return new ConveyorDomain(data);
        }
    }
}
