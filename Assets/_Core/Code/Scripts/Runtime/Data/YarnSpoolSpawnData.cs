using UnityEngine;

namespace WoolGame
{
    [System.Serializable]
    public sealed class YarnSpoolSpawnData : EntitySpawnData
    {
        public Color yarnColor = Color.white;
        public int chargeCount = 1;
        public float conveyorProgress;
        public float conveyorSpeed = 0.15f;
    }
}
