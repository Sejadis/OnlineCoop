namespace DefaultNamespace
{
    public abstract class Runnable
    {
        public float StartTime { get; set; } = -1f;
        public bool IsStarted => StartTime > 0;
        
        public abstract bool Start();

        public abstract bool Update();

        public abstract void End();

        public abstract void Cancel();
    }
}