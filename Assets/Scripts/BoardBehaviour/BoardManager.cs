using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace GameDev.Behaviour2D.Puzzle.Board
{
    public class BoardManager : MonoBehaviour
    {
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;
        [SerializeField] private Cell _cellPrefab;
        public Vector2 BoardDimension=> new Vector2(_gridWidth, _gridHeight);
        private void Start()
        {
            SetUpBoard();
        }
        private void SetUpBoard()
        {
            for (int i = 0; i < _gridWidth; i++)
            {
                for (int j = 0; j < _gridHeight; j++)
                {
                    Cell cell = Instantiate(_cellPrefab, new Vector3(i, j, 0), Quaternion.identity).GetComponent<Cell>();
                    cell.transform.SetParent(transform);
                    cell?.Init(this, i, j);
                }
            }
        }
    }
}

