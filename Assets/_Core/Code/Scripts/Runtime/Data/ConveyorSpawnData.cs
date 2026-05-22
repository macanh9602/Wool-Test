namespace WoolGame
{
    [System.Serializable]
    public sealed class ConveyorSpawnData : EntitySpawnData
    {
        public float startProgress;
        public float speed = 0.15f;
        public int direction = 1;
        public bool loop = true;
        public bool startPaused;
    }
}
