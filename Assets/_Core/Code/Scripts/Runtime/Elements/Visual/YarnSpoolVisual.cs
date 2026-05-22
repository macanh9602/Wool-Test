using Dreamteck.Splines;
using UnityEngine;

namespace WoolGame
{
    public sealed class YarnSpoolVisual : EntityVisualBase<YarnSpoolDomain>
    {
        [SerializeField] private SplineComputer conveyorSpline;
        [SerializeField] private Renderer colorRenderer;

        public void SetConveyorSpline(SplineComputer spline)
        {
            conveyorSpline = spline;
        }

        protected override void HandleProgressChanged(ProgressChangedEvent evt)
        {
            if (conveyorSpline == null)
            {
                return;
            }

            var sample = conveyorSpline.Evaluate(evt.Progress);
            transform.SetPositionAndRotation(sample.position, sample.rotation);
        }

        protected override void HandleColorChanged(ColorChangedEvent evt)
        {
            if (colorRenderer == null)
            {
                return;
            }

            colorRenderer.material.color = evt.Color;
        }
    }
}
