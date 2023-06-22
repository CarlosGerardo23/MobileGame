using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
namespace GameDev.Behaviour2D.Puzzle.Board
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private int _gridWidth;
        [SerializeField] private int _gridHeight;
        [SerializeField] private Cell _cellPrefab;
        [SerializeField] private bool _useDelay;
        [SerializeField] private float _delayTime;
        [SerializeField] private bool _useInputModifications;
        [SerializeField] private UnityEvent _onBoardFinish;
        private Cell[,] _cellsArray;
        public Vector2 BoardDimension => new Vector2(_gridWidth, _gridHeight);
        public int BoardWidth => _gridWidth;
        public int BoardHeight => _gridHeight;
        private void Start()
        {
            StartCoroutine(SetUpBoard());
        }

        public IEnumerator SetUpCellsByPosition(List<Vector2> positions)
        {
            if (_useInputModifications)
                BoardCellMovementController.DisableControlls();
            for (int i = 0; i < positions.Count; i++)
            {
                CreateCell((int)positions[i].x, (int)positions[i].y);
                if (_useDelay)
                    yield return new WaitForSeconds(_delayTime);
            }
            if (_useInputModifications)
                BoardCellMovementController.EnableControlls();
        }
        private IEnumerator SetUpBoard()
        {
            if (_useInputModifications)
                BoardCellMovementController.DisableControlls();
            _cellsArray = new Cell[_gridWidth, _gridHeight];
            for (int i = 0; i < _gridWidth; i++)
            {
                for (int j = 0; j < _gridHeight; j++)
                {
                    CreateCell(i, j);
                    if (_useDelay)
                        yield return new WaitForSeconds(_delayTime);
                }
            }
            if (_useInputModifications)
                BoardCellMovementController.EnableControlls();
            _onBoardFinish?.Invoke();
        }

        private void CreateCell(int width, int height)
        {
            Cell cell = Instantiate(_cellPrefab, new Vector3(width, height, 0), Quaternion.identity).GetComponent<Cell>();
            cell.transform.SetParent(transform);
            cell.name = $"Cell {width},{height}";
            cell?.Init(this, width, height);
            _cellsArray[width, height] = cell;
        }
        public void SwapCells(Vector2 firstIndex, Vector2 secondIndex)
        {
            Cell firstCell = _cellsArray[(int)firstIndex.x, (int)firstIndex.y];
            Cell secondCell = _cellsArray[(int)secondIndex.x, (int)secondIndex.y];

            _cellsArray[(int)firstIndex.x, (int)firstIndex.y] = secondCell;
            _cellsArray[(int)secondIndex.x, (int)secondIndex.y] = firstCell;
            if (secondCell != null)
                secondCell.name = $"Cell {firstIndex.x},{firstIndex.y}";
            if (firstCell != null)
                firstCell.name = $"Cell {secondIndex.x},{secondIndex.y}";

        }

        public bool TryGetCellByPosition(Vector2 position, out Cell result)
        {
            result = null;
            if (position.x >= 0 && position.x < _gridWidth && position.y >= 0 && position.y < _gridHeight)
            {
                result = _cellsArray[(int)position.x, (int)position.y];

                return result != null;
            }
            return false;
        }

        public void DestroyCellByPosition(Vector2 position)
        {
            int width = (int)position.x;
            int height = (int)position.y;
            Cell cell = _cellsArray[width, height];
            if (cell != null)
            {
                _cellsArray[width, height] = null;
                cell.DestroyCell();
            }
        }
    }
}

