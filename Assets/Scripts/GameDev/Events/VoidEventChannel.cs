using GameDev.Behaviour2D.Puzzle;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameDev.Events
{
    [CreateAssetMenu(menuName ="Events/Basic")]
    public class VoidEventChannel : EventChannel
    {
        Action _action;
        public void Subscribe(Action action) => _action += action;

        public void Unsubscribe(Action action) => _action -= action;

        public void Raise() => _action?.Invoke();
    }
}

