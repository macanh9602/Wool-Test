using UnityEngine;

namespace WoolGame
{
    public sealed class WoolBlockDomain : EntityDomainBase
    {
        public WoolBlockDomain(WoolBlockSpawnData data) : base(data.entityId)
        {
            RequiredColor = data.requiredColor;
            RequiredCharge = Mathf.Max(1, data.requiredCharge);
        }

        public Color RequiredColor { get; }
        public int RequiredCharge { get; private set; }

        public override void OnCreated()
        {
            base.OnCreated();
            EmitColor(RequiredColor);
        }

        public bool CanAccept(Color yarnColor)
        {
            return RequiredCharge > 0 && yarnColor == RequiredColor;
        }

        public void ApplyCharge()
        {
            if (RequiredCharge <= 0)
            {
                return;
            }

            RequiredCharge--;

            if (RequiredCharge == 0)
            {
                EmitClearStarted();
                EmitClearCompleted();
                Dispose();
            }
        }
    }
}
