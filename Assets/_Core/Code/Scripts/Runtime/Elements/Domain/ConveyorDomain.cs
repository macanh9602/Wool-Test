using UnityEngine;

namespace WoolGame
{
    public sealed class ConveyorDomain : EntityDomainBase
    {
        private readonly bool loop;
        private readonly int direction;

        public ConveyorDomain(ConveyorSpawnData data) : base(data.entityId)
        {
            Progress = Mathf.Clamp01(data.startProgress);
            Speed = Mathf.Max(0f, data.speed);
            direction = data.direction >= 0 ? 1 : -1;
            loop = data.loop;
            IsPaused = data.startPaused;
        }

        public float Progress { get; private set; }
        public float Speed { get; private set; }
        public bool IsPaused { get; private set; }

        public override void OnCreated()
        {
            base.OnCreated();
            EmitProgress(Progress, Vector3.zero);
        }

        public override void Tick(float deltaTime)
        {
            if (IsPaused || State != EntityState.Active)
            {
                return;
            }

            SetProgress(Progress + Speed * direction * deltaTime);
        }

        public void SetSpeed(float speed)
        {
            Speed = Mathf.Max(0f, speed);
        }

        public void Pause()
        {
            IsPaused = true;
            SetState(EntityState.Paused);
        }

        public void Resume()
        {
            IsPaused = false;
            SetState(EntityState.Active);
        }

        private void SetProgress(float progress)
        {
            Progress = loop ? Mathf.Repeat(progress, 1f) : Mathf.Clamp01(progress);
            EmitProgress(Progress, Vector3.zero);
        }
    }
}
