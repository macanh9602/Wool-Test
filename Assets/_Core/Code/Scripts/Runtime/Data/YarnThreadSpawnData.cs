using UnityEngine;

namespace WoolGame
{
    [System.Serializable]
    public sealed class YarnThreadSpawnData : EntitySpawnData
    {
        public Color yarnColor = Color.white;
        public Transform startAnchor;
        public Transform endAnchor;
    }
}
