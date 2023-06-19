using GameDev.Behaviour2D.Puzzle.Board;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameDev.Behaviour2D.Puzzle
{
    public class Cell : MonoBehaviour
    {
        protected BoardController board;
        private int _positionX;
        private int _positionY;

        public Vector2 CellPosition=> new Vector2(_positionX, _positionY);
        public virtual void Init(BoardController board, int positionX, int positionY)
        {
            _positionX= positionX;
            _positionY= positionY;
            this.board= board;
        }

        public void UpdateCells(int newPosX, int newPosY)
        {
            _positionX= newPosX;
            _positionY= newPosY;
        }
        public virtual void DestroyCell()
        {
            Destroy(gameObject);
        }
    }

}
