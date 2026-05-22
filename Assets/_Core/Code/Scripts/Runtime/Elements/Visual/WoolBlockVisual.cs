using UnityEngine;

namespace WoolGame
{
    public sealed class WoolBlockVisual : EntityVisualBase<WoolBlockDomain>
    {
        [SerializeField] private Renderer colorRenderer;

        private void Awake()
        {
            EnsureRenderer();
        }

        public override void Bind(WoolBlockDomain domain)
        {
            EnsureRenderer();
            base.Bind(domain);
        }

        protected override void HandleColorChanged(ColorChangedEvent evt)
        {
            if (colorRenderer == null)
            {
                return;
            }

            colorRenderer.material.color = evt.Color;
        }

        protected override void HandleDestroyed()
        {
            Destroy(gameObject);
        }

        private void EnsureRenderer()
        {
            if (colorRenderer != null)
            {
                return;
            }

            var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
            visual.name = "Wool Block Mesh";
            visual.transform.SetParent(transform, false);
            visual.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            var collider = visual.GetComponent<Collider>();
            if (collider != null)
            {
                Destroy(collider);
            }

            colorRenderer = visual.GetComponent<Renderer>();
        }
    }
}
