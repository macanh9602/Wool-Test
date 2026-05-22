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
            movingRoot.position = sample.position;

            if (sample.forward.sqrMagnitude > 0.0001f && sample.up.sqrMagnitude > 0.0001f)
            {
                movingRoot.rotation = sample.rotation;
            }
        }
    }
}
