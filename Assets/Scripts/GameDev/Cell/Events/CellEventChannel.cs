using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Behaviour2D.Puzzle;
namespace GameDev.Behaviour2D.Puzzle.Events
{
    [CreateAssetMenu(menuName = "Events/Board/Cell Event")]
    public class CellEventChannel : EventChannel
    {
        private Action<Cell> _action;

        public void Subscribe(Action<Cell> action)=> _action += action; 
        public void Unsubscribe(Action<Cell> action)=> _action -= action;
        public void Raise(Cell cell) => _action?.Invoke(cell);
   
    }

}
