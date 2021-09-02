using System.Collections.Generic;
using UnityEngine;

namespace Runnable
{
    public abstract class Runner<T, TY> where T : Runnable where TY : struct
    {
        private readonly List<T> currentRunnables = new List<T>();
        protected List<T> CurrentRunnables => currentRunnables;

        public virtual void AddRunnable(ref TY runtimeParameter)
        {
            var runnable = GetRunnable(ref runtimeParameter);
            currentRunnables.Add(runnable);
            if (currentRunnables.Count == 1)
            {
                StartRunnable();
            }
        }

        private void StartRunnable()
        {
            if (currentRunnables.Count > 0)
            {
                var runnable = currentRunnables[0];
                runnable.StartTime = Time.time;
                var shouldEnd = !runnable.Start();
                if (shouldEnd)
                {
                    runnable.End();
                    currentRunnables.Remove(runnable);
                }
            }
        }

        public virtual void Update()
        {
            for (int i = CurrentRunnables.Count - 1; i >= 0; i--)
            {
                if (!UpdateRunnable(CurrentRunnables[i]))
                {
                    CurrentRunnables[i].End();
                    CurrentRunnables.RemoveAt(i);
                }
            }
        }

        protected virtual bool UpdateRunnable(T runnable)
        {
            return runnable.Update();
        }

        protected abstract T GetRunnable(ref TY runtimeParams);
    }
}