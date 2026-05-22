using UnityEngine;

namespace WoolGame
{
    public sealed class YarnThreadDomain : EntityDomainBase
    {
        public YarnThreadDomain(YarnThreadSpawnData data) : base(data.entityId)
        {
            YarnColor = data.yarnColor;
            StartAnchor = data.startAnchor;
            EndAnchor = data.endAnchor;
        }

        public Color YarnColor { get; }
        public Transform StartAnchor { get; }
        public Transform EndAnchor { get; }

        public override void OnCreated()
        {
            base.OnCreated();
            EmitColor(YarnColor);
        }

        public void StartClear()
        {
            EmitClearStarted();
        }

        public void CompleteClear()
        {
            EmitClearCompleted();
        }
    }
}
