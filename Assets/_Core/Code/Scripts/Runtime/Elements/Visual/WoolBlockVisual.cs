using UnityEngine;

namespace WoolGame
{
    public sealed class WoolBlockVisual : EntityVisualBase<WoolBlockDomain>
    {
        [SerializeField] private Renderer colorRenderer;

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
    }
}
