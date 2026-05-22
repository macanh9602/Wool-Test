using UnityEngine;

namespace WoolGame
{
    public sealed class YarnThreadVisual : EntityVisualBase<YarnThreadDomain>
    {
        [SerializeField] private LineRenderer lineRenderer;

        private void LateUpdate()
        {
            if (Domain == null || lineRenderer == null || Domain.StartAnchor == null || Domain.EndAnchor == null)
            {
                return;
            }

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, Domain.StartAnchor.position);
            lineRenderer.SetPosition(1, Domain.EndAnchor.position);
        }

        protected override void HandleColorChanged(ColorChangedEvent evt)
        {
            if (lineRenderer == null)
            {
                return;
            }

            lineRenderer.startColor = evt.Color;
            lineRenderer.endColor = evt.Color;
        }

        protected override void HandleDestroyed()
        {
            Destroy(gameObject);
        }
    }
}
