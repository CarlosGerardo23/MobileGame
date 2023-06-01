using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace GameDev.Behaviour2D.Puzzle.Board
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;
        [SerializeField] private Cell _cellPrefab;
        private Cell[,] _cellsArray;
        public Vector2 BoardDimension => new Vector2(_gridWidth, _gridHeight);
        private void Start()
        {
            SetUpBoard();
        }
        private void SetUpBoard()
        {
            _cellsArray = new Cell[_gridWidth, _gridHeight];
            for (int i = 0; i < _gridWidth; i++)
            {
                for (int j = 0; j < _gridHeight; j++)
                {
                    Cell cell = Instantiate(_cellPrefab, new Vector3(i, j, 0), Quaternion.identity).GetComponent<Cell>();
                    cell.transform.SetParent(transform);
                    cell.name = $"Cell {i},{j}";
                    cell?.Init(this, i, j);
                    _cellsArray[i, j] = cell;
                }
            }
        }
        public void SwapCells(Vector2 firstIndex, Vector2 secondIndex)
        {
            Cell firstCell = _cellsArray[(int)firstIndex.x, (int)firstIndex.y];
            Cell secondCell = _cellsArray[(int)secondIndex.x, (int)secondIndex.y];

            _cellsArray[(int)firstIndex.x, (int)firstIndex.y] = secondCell;
            _cellsArray[(int)secondIndex.x, (int)secondIndex.y] = firstCell;
        }
    }
}

