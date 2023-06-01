using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDev.Behaviour2D.Puzzle.Movement
{
    [RequireComponent(typeof(Cell))]
    public class CellMovementController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 0.25f;
        private Cell _cell;

        private void Awake()
        {
            _cell = GetComponent<Cell>();
        }
        public void Move(int destX, int destY)
        {
            transform.DOMove(new Vector3(destX, destY, 0), _movementSpeed).SetEase(Ease.InOutCubic).onComplete =
                () =>
                {
                    _cell.UpdateCells(destX, destY);
                };
        }
        public void Move(Vector3 position)
        {
            transform.DOMove(position, _movementSpeed).SetEase(Ease.InOutCubic).onComplete =
                () =>
                {
                    _cell.UpdateCells((int)position.x, (int)position.y);
                };
        }
        [ContextMenu("Test move")]
        public void MoveTest()
        {
            Move(0, 0);
        }
    }
}

