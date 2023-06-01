using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Behaviour2D.Controls;
using GameDev.Behaviour2D.Puzzle.Movement;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.UIElements;


namespace GameDev.Behaviour2D.Puzzle.Board
{
    [RequireComponent(typeof(BoardController))]
    public class BoardCellMovementController : MonoBehaviour
    {
        [SerializeField] private InputTouchControls _touchControls;

        private BoardController _boardController;
        private CellMovementController _currentCell;
        private CellMovementController _endCell;
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
            OnPointerClick(position);
        }
        private void TouchUp(Vector2 position)
        {
            GetLastCell(position);
            OnPointerUp();
        }
        private void OnPointerClick(Vector2 position)
        {
            if (position.x > Screen.width|| position.y > Screen.height || position.x < 0 || position.y < 0 ) return;
            Vector3 worlPosition = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(worlPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Se tocó el objeto: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent(out CellMovementController cell))
                {
                    print($"Current cell {cell.name}");
                    _currentCell = cell;
                }
            }

        }
        private void GetLastCell(Vector2 position)
        {
            print("Start click");
            Vector3 worlPosition = UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 0));
            RaycastHit2D hit = Physics2D.Raycast(worlPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Se tocó el objeto: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.TryGetComponent(out CellMovementController cell))
                {
                    print($"Current cell {cell.name}");
                    _endCell = cell;
                }
            }

        }

        private void OnPointerUp()
        {
            if (_currentCell != null && _endCell != null)
            {
                Cell moveCurrent = _currentCell.GetComponent<Cell>();
                Cell moveEnd = _endCell.GetComponent<Cell>();
                _boardController.SwapCells(moveCurrent.CellPosition, moveEnd.CellPosition);
                _currentCell?.Move(_endCell.transform.position);
                _endCell?.Move(_currentCell.transform.position);

            }
            _currentCell = null;
            _endCell = null;
        }
    }
}

