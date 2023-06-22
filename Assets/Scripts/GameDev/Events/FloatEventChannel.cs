using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameDev.Events
{
    [CreateAssetMenu(menuName = "Events/Float")]
    public class FloatEventChannel : EventChannel
    {
        Action<float> _action;
        public void Subscribe(Action<float> action) => _action += action;

        public void Unsubscribe(Action<float> action) => _action -= action;

        public void Raise(float number) => _action?.Invoke(number);
    }
}

