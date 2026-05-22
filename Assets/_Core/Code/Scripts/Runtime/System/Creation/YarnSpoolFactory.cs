using Dreamteck.Splines;
using UnityEngine;

namespace WoolGame
{
    public sealed class YarnSpoolFactory : EntityFactoryBase<YarnSpoolSpawnData, YarnSpoolDomain, YarnSpoolVisual>
    {
        [SerializeField] private SplineComputer defaultConveyorSpline;

        public void SetDefaultConveyorSpline(SplineComputer spline)
        {
            defaultConveyorSpline = spline;
        }

        protected override YarnSpoolDomain CreateDomain(YarnSpoolSpawnData data)
        {
            return new YarnSpoolDomain(data);
        }

        protected override void ConfigureVisual(YarnSpoolVisual visual, YarnSpoolDomain domain, YarnSpoolSpawnData data)
        {
            visual.SetConveyorSpline(defaultConveyorSpline);
        }
    }
}
