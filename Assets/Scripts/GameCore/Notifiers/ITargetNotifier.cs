using System;
using UnityEngine;

namespace GameCore.Notifiers
{
    public interface ITargetNotifier
    {
        event Action<GameObject> OnTargetEnter;
        event Action<GameObject> OnTargetExit;
        
        void UpdateNotifierRadius(float radius);
    }
}