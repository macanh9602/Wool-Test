using UnityEngine;

namespace WoolGame
{
    public abstract class EntityVisualBase<TDomain> : MonoBehaviour where TDomain : EntityDomainBase
    {
        protected TDomain Domain { get; private set; }

        public virtual void Bind(TDomain domain)
        {
            Unbind();

            Domain = domain;
            Domain.Created += HandleCreated;
            Domain.StateChanged += HandleStateChanged;
            Domain.ProgressChanged += HandleProgressChanged;
            Domain.ColorChanged += HandleColorChanged;
            Domain.ClearStarted += HandleClearStarted;
            Domain.ClearCompleted += HandleClearCompleted;
            Domain.ReturnedToSlot += HandleReturnedToSlot;
            Domain.Destroyed += HandleDestroyed;
        }

        public virtual void Unbind()
        {
            if (Domain == null)
            {
                return;
            }

            Domain.Created -= HandleCreated;
            Domain.StateChanged -= HandleStateChanged;
            Domain.ProgressChanged -= HandleProgressChanged;
            Domain.ColorChanged -= HandleColorChanged;
            Domain.ClearStarted -= HandleClearStarted;
            Domain.ClearCompleted -= HandleClearCompleted;
            Domain.ReturnedToSlot -= HandleReturnedToSlot;
            Domain.Destroyed -= HandleDestroyed;
            Domain = null;
        }

        protected virtual void OnDestroy()
        {
            Unbind();
        }

        protected virtual void HandleCreated(IEntityDomain domain) { }
        protected virtual void HandleStateChanged(EntityState state) { }
        protected virtual void HandleProgressChanged(ProgressChangedEvent evt) { }
        protected virtual void HandleColorChanged(ColorChangedEvent evt) { }
        protected virtual void HandleClearStarted() { }
        protected virtual void HandleClearCompleted() { }
        protected virtual void HandleReturnedToSlot() { }
        protected virtual void HandleDestroyed() { }
    }
}
