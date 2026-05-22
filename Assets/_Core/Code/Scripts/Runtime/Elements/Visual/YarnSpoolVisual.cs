using Dreamteck.Splines;
using UnityEngine;

namespace WoolGame
{
    public sealed class YarnSpoolVisual : EntityVisualBase<YarnSpoolDomain>
    {
        [SerializeField] private SplineComputer conveyorSpline;
        [SerializeField] private Renderer colorRenderer;

        private void Awake()
        {
            EnsureRenderer();
        }

        public void SetConveyorSpline(SplineComputer spline)
        {
            conveyorSpline = spline;
        }

        public override void Bind(YarnSpoolDomain domain)
        {
            EnsureRenderer();
            base.Bind(domain);
        }

        protected override void HandleProgressChanged(ProgressChangedEvent evt)
        {
            if (conveyorSpline == null)
            {
                return;
            }

            var sample = conveyorSpline.Evaluate(evt.Progress);
            transform.position = sample.position;

            if (sample.forward.sqrMagnitude > 0.0001f && sample.up.sqrMagnitude > 0.0001f)
            {
                transform.rotation = sample.rotation;
            }
        }

        protected override void HandleColorChanged(ColorChangedEvent evt)
        {
            if (colorRenderer == null)
            {
                return;
            }

            colorRenderer.material.color = evt.Color;
        }

        private void EnsureRenderer()
        {
            if (colorRenderer != null)
            {
                return;
            }

            var visual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            visual.name = "Yarn Spool Mesh";
            visual.transform.SetParent(transform, false);
            visual.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);

            var collider = visual.GetComponent<Collider>();
            if (collider != null)
            {
                Destroy(collider);
            }

            colorRenderer = visual.GetComponent<Renderer>();
        }
    }
}
