using UnityEngine;
using GameDev.Behaviour2D.Controls;
using GameDev.Behaviour2D.Puzzle.Movement;

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
        private void Awake()
        {
            _boardController = GetComponent<BoardController>();
        }

        private void OnEnable()
        {
            _touchControls.OnStartedTouchPosition += TouchDown;
            _touchControls.OnEndedTouchPosition += TouchUp;

        }
        private void TouchDown(Vector2 position)
        {
            if (position.x > Screen.width || position.y > Screen.height || position.x < 0 || position.y < 0 ||
            position.x == Mathf.Infinity || position.y == Mathf.Infinity || position.x == -Mathf.Infinity || position.y == -Mathf.Infinity) return;
            OnPointerDown(position);
        }
        private void TouchUp(Vector2 position)
        {
            if (position.x > Screen.width || position.y > Screen.height || position.x < 0 || position.y < 0 ||
              position.x == Mathf.Infinity || position.y == Mathf.Infinity || position.x == -Mathf.Infinity || position.y == -Mathf.Infinity) return;
            GetLastCell(position);
            OnPointerUp();
        }
        private void OnPointerDown(Vector2 position)
        {
            _startCellIndex = new Vector2(-1, -1);
            Vector3 worlPosition = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(worlPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Se tocó el objeto: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                {
                    print($"Current cell {cell.name}");
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
                Debug.Log("Se tocó el objeto: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent(out Cell cell))
                {
                    print($"last cell {cell.name}");
                    _endCellIndex = cell.CellPosition;
                }
            }
        }
        private void OnPointerUp()
        {
            if (_startCellIndex != new Vector2(-1, -1) && _endCellIndex != new Vector2(-1, -1))
            {
                if (!_boardController.TryGetCellByPosition(_startCellIndex, out Cell startCell))
                    return;
                if (!_boardController.TryGetCellByPosition(_endCellIndex, out Cell endCell))
                    return;

                if (_useOneTileRestriction && !CheckPosibleMove(_startCellIndex, _endCellIndex))
                {
                    _startCellIndex = new Vector2(-1, -1);
                    _endCellIndex = new Vector2(-1, -1);
                    return;
                }
                _boardController.SwapCells(_startCellIndex, _endCellIndex);

                startCell?.GetComponent<CellMovementController>()?.Move(endCell.transform.position, true);
                endCell?.GetComponent<CellMovementController>()?.Move(startCell.transform.position, true);

            }
            else
            {
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
                    return;
                if (!_boardController.TryGetCellByPosition(_endCellIndex, out Cell endCell))
                    return;

                if (_useOneTileRestriction && !CheckPosibleMove(_startCellIndex, _endCellIndex))
                {
                    _startCellIndex = new Vector2(-1, -1);
                    _endCellIndex = new Vector2(-1, -1);
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

        }
    }
}

