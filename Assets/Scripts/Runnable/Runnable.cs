using System;

namespace Runnable
{
    public abstract class Runnable
    {
        public float StartTime { get; set; } = -1f;
        public bool IsStarted => StartTime > 0;
        
        public abstract bool Start();

        public abstract bool Update();

        public abstract void End();

        public abstract void Cancel();
        public virtual bool Reactivate()
        {
            throw new NotSupportedException(
                $"Can not call Runnable.Reactivate() on an class that doesnt support it");
        }
    }
}