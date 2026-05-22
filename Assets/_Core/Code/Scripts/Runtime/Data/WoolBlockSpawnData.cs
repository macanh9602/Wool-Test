using UnityEngine;

namespace WoolGame
{
    [System.Serializable]
    public sealed class WoolBlockSpawnData : EntitySpawnData
    {
        public Color requiredColor = Color.white;
        public int requiredCharge = 1;
    }
}
