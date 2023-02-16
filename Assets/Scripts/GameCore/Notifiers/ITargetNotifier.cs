using System;
using UnityEngine;

namespace GameCore.Notifiers
{
    public interface ITargetNotifier<T>
    {
        event Action<T> OnTargetEnter;
        event Action<T> OnTargetExit;
        
        void UpdateNotifierRadius(float radius);
    }
}