using System;

namespace GameCore.Notifiers
{
    public interface ITargetNotifier<out T>
    {
        event Action<T> OnTargetEnter;
        event Action<T> OnTargetExit;
        
        void UpdateNotifierRadius(float radius);
    }
}