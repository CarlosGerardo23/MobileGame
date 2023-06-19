using UnityEngine;
using GameDev.Behaviour2D.Controls;
using GameDev.Behaviour2D.Puzzle.Movement;
using System.Collections.Generic;
namespace GameDev.Behaviour2D.Puzzle.Board
{
    [RequireComponent(typeof(BoardController))]
    public class BoardCellMovementController : MonoBehaviour
    {
        [SerializeField] private InputTouchControls _touchControls;
        [SerializeField] private bool _useOneTileRestriction;

        private BoardController _boardController;
        private Vector2 _startCellIndex;
        private Vector2 _endCellIndex;
        private static bool _canMove;
        private void Awake()
        {
            _boardController = GetComponent<BoardController>();
        }

        private void OnEnable()
        {
            _touchControls.OnStartedTouchPosition += TouchDown;
            _touchControls.OnEndedTouchPosition += TouchUp;

        }
        private void Update()
        {
            print(_canMove);
        }
        private void TouchDown(Vector2 position)
        {
            if (!_canMove) return;
            if (position.x > Screen.width || position.y > Screen.height || position.x < 0 || position.y < 0 ||
            position.x == Mathf.Infinity || position.y == Mathf.Infinity || position.x == -Mathf.Infinity || position.y == -Mathf.Infinity) return;
            OnPointerDown(position);
        }
        private void TouchUp(Vector2 position)
        {
            if (!_canMove) return;
            if (position.x > Screen.width || position.y > Screen.height || position.x < 0 || position.y < 0 ||
              position.x == Mathf.Infinity || position.y == Mathf.Infinity || position.x == -Mathf.Infinity || position.y == -Mathf.Infinity) return;
            GetLastCell(position);
            OnPointerUp();
        }
        private void OnPointerDown(Vector2 position)
        {
            if (!_canMove) return;
            _startCellIndex = new Vector2(-1, -1);
            Vector3 worlPosition = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(worlPosition, Vector2.zero);
            if (hit.collider != null)
            {

                if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                {

                    _startCellIndex = cell.CellPosition;
                }
            }

        }
        private void GetLastCell(Vector2 position)
        {
            _endCellIndex = new Vector2(-1, -1);
            Vector3 worlPosition = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(worlPosition, Vector2.zero);
            if (hit.collider != null)
            {

                if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                {

                    _endCellIndex = cell.CellPosition;
                }
            }
        }
        private void OnPointerUp()
        {
            if (_startCellIndex != new Vector2(-1, -1) && _endCellIndex != new Vector2(-1, -1))
            {
                DisableControlls();

                if (!_boardController.TryGetCellByPosition(_startCellIndex, out Cell startCell))
                {
                    EnableControlls();
                    return;
                }
                if (!_boardController.TryGetCellByPosition(_endCellIndex, out Cell endCell))
                {
                    EnableControlls();
                    return;
                }

                if (_useOneTileRestriction && !CheckPosibleMove(_startCellIndex, _endCellIndex))
                {
                    _startCellIndex = new Vector2(-1, -1);
                    _endCellIndex = new Vector2(-1, -1);
                    EnableControlls();
                    return;
                }
                _boardController.SwapCells(_startCellIndex, _endCellIndex);

                startCell?.GetComponent<CellMovementController>()?.Move(endCell.transform.position, true);
                endCell?.GetComponent<CellMovementController>()?.Move(startCell.transform.position, true);

            }
            else
            {
                EnableControlls();
                _startCellIndex = new Vector2(-1, -1);
                _endCellIndex = new Vector2(-1, -1);
            }
        }
        private bool CheckPosibleMove(Vector2 start, Vector2 end)
        {

            if (Mathf.Abs(start.x - end.x) == 1 && start.y == end.y)
                return true;
            if (Mathf.Abs(start.y - end.y) == 1 && start.x == end.x)
                return true;
            return false;
        }
        public void MoveBackCells()
        {
            if (_startCellIndex != new Vector2(-1, -1) && _endCellIndex != new Vector2(-1, -1))
            {
                if (!_boardController.TryGetCellByPosition(_startCellIndex, out Cell startCell))
                {
                    EnableControlls();
                    return;
                }
                if (!_boardController.TryGetCellByPosition(_endCellIndex, out Cell endCell))
                {
                    EnableControlls();
                    return;
                }

                if (_useOneTileRestriction && !CheckPosibleMove(_startCellIndex, _endCellIndex))
                {
                    _startCellIndex = new Vector2(-1, -1);
                    _endCellIndex = new Vector2(-1, -1);
                    EnableControlls();
                    return;
                }
                _boardController.SwapCells(_startCellIndex, _endCellIndex);

                startCell?.GetComponent<CellMovementController>()?.Move(endCell.transform.position, false);
                endCell?.GetComponent<CellMovementController>()?.Move(startCell.transform.position, false);

            }
            else
            {
                _startCellIndex = new Vector2(-1, -1);
                _endCellIndex = new Vector2(-1, -1);
            }
            EnableControlls();

        }
        public void CollapseBoard(List<Vector2> cellsToCollapse, out List<Vector2> cellsCollpasedList,
            out List<Vector2> cellsOriginalPosition)
        {
            cellsCollpasedList = new List<Vector2>();
            cellsOriginalPosition = new List<Vector2>();
            List<Vector2> cellsCollapsedOriginalPosition = new List<Vector2>();
            int positionX;
            int positionY;

            for (int i = 0; i < cellsToCollapse.Count; i++)
            {
                positionX = (int)cellsToCollapse[i].x;
                positionY = (int)cellsToCollapse[i].y;

                for (int j = positionY; j < _boardController.BoardHeight; j++)
                {
                    if (!_boardController.TryGetCellByPosition(new Vector2(positionX, j), out _))
                    {
                        for (int k = j; k < _boardController.BoardHeight; k++)
                        {
                            if (_boardController.TryGetCellByPosition(new Vector2(positionX, k), out Cell cell))
                            {
                                Debug.Log($"The cell {cell.CellPosition} is going to {new Vector2(positionX, j)}");
                                cellsCollapsedOriginalPosition.Add(cell.CellPosition);
                                _boardController.SwapCells(new Vector2(positionX, j), cell.CellPosition);
                                cell.GetComponent<CellMovementController>().Move(positionX, j, false);
                                cellsCollpasedList.Add(new Vector2(positionX, j));
                                break;
                            }
                            else
                                cellsCollapsedOriginalPosition.Add(new Vector2(positionX, k));
                        }
                    }
                }
            }
            for (int i = 0; i < cellsCollapsedOriginalPosition.Count; i++)
            {
                if (!_boardController.TryGetCellByPosition(cellsCollapsedOriginalPosition[i], out _))
                {
                    cellsOriginalPosition.Add(cellsCollapsedOriginalPosition[i]);
                }
            }
        }
        public static void EnableControlls()
        {
            _canMove = true;
        }
        public static void DisableControlls()
        {
            _canMove = false;
        }
    }


}

