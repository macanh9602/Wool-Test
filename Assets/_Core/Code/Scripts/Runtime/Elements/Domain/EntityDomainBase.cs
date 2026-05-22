using System;
using UnityEngine;

namespace WoolGame
{
    public abstract class EntityDomainBase : IEntityDomain, IEntityDomainEvents
    {
        public event Action<IEntityDomain> Created;
        public event Action<EntityState> StateChanged;
        public event Action<ProgressChangedEvent> ProgressChanged;
        public event Action<ColorChangedEvent> ColorChanged;
        public event Action ClearStarted;
        public event Action ClearCompleted;
        public event Action ReturnedToSlot;
        public event Action Destroyed;

        protected EntityDomainBase(int entityId)
        {
            EntityId = entityId;
        }

        public int EntityId { get; }
        public EntityState State { get; private set; } = EntityState.None;

        public virtual void OnCreated()
        {
            SetState(EntityState.Created);
            Created?.Invoke(this);
            SetState(EntityState.Active);
        }

        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void Dispose()
        {
            SetState(EntityState.Destroyed);
            Destroyed?.Invoke();
        }

        protected void SetState(EntityState state)
        {
            if (State == state)
            {
                return;
            }

            State = state;
            StateChanged?.Invoke(State);
        }

        protected void EmitProgress(float progress, Vector3 worldPosition)
        {
            ProgressChanged?.Invoke(new ProgressChangedEvent(progress, worldPosition));
        }

        protected void EmitColor(Color color)
        {
            ColorChanged?.Invoke(new ColorChangedEvent(color));
        }

        protected void EmitClearStarted()
        {
            SetState(EntityState.Clearing);
            ClearStarted?.Invoke();
        }

        protected void EmitClearCompleted()
        {
            ClearCompleted?.Invoke();
            SetState(EntityState.Active);
        }

        protected void EmitReturnedToSlot()
        {
            SetState(EntityState.ReturnedToSlot);
            ReturnedToSlot?.Invoke();
        }
    }
}
