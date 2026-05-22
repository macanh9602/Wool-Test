using Dreamteck.Splines;
using UnityEngine;

namespace WoolGame
{
    public sealed class ConveyorVisual : EntityVisualBase<ConveyorDomain>
    {
        [SerializeField] private SplineComputer spline;
        [SerializeField] private Transform movingRoot;

        public SplineComputer Spline => spline;

        protected override void HandleProgressChanged(ProgressChangedEvent evt)
        {
            if (spline == null || movingRoot == null)
            {
                return;
            }

            var sample = spline.Evaluate(evt.Progress);
            movingRoot.SetPositionAndRotation(sample.position, sample.rotation);
        }
    }
}
