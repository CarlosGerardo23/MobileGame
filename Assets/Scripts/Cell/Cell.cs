using GameDev.Behaviour2D.Puzzle.Board;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameDev.Behaviour2D.Puzzle
{
    public class Cell : MonoBehaviour
    {
        private BoardManager _board;
        private int _positionX;
        private int _positionY;

        public virtual void Init(BoardManager board, int positionX, int positionY)
        {
            _positionX= positionX;
            _positionY= positionY;
            _board= board;
        }
    }

}
