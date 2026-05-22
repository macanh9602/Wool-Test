using UnityEngine;

namespace WoolGame
{
    public sealed class YarnSpoolDomain : EntityDomainBase
    {
        public YarnSpoolDomain(YarnSpoolSpawnData data) : base(data.entityId)
        {
            YarnColor = data.yarnColor;
            ChargeCount = Mathf.Max(0, data.chargeCount);
            ConveyorProgress = Mathf.Clamp01(data.conveyorProgress);
            ConveyorSpeed = Mathf.Max(0f, data.conveyorSpeed);
        }

        public Color YarnColor { get; private set; }
        public int ChargeCount { get; private set; }
        public float ConveyorProgress { get; private set; }
        public float ConveyorSpeed { get; private set; }

        public override void OnCreated()
        {
            base.OnCreated();
            EmitColor(YarnColor);
            EmitProgress(ConveyorProgress, Vector3.zero);
        }

        public override void Tick(float deltaTime)
        {
            if (State != EntityState.Active)
            {
                return;
            }

            ConveyorProgress = Mathf.Repeat(ConveyorProgress + ConveyorSpeed * deltaTime, 1f);
            EmitProgress(ConveyorProgress, Vector3.zero);
        }

        public bool CanClear(WoolBlockDomain target)
        {
            return target != null && ChargeCount > 0 && target.CanAccept(YarnColor);
        }

        public void ConsumeCharge()
        {
            if (ChargeCount <= 0)
            {
                return;
            }

            EmitClearStarted();
            ChargeCount--;
            EmitClearCompleted();
        }

        public void ReturnToSlot()
        {
            EmitReturnedToSlot();
        }
    }
}
