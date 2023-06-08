using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Behaviour2D.Puzzle.Events;

namespace GameDev.Behaviour2D.Puzzle.Movement
{
    [RequireComponent(typeof(Cell))]
    public class CellMovementController : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 0.25f;
        [SerializeField] private CellEventChannel _onFinishMovingSubject;
        private Cell _cell;

        private void Awake()
        {
            _cell = GetComponent<Cell>();
        }
        public void Move(int destX, int destY, bool moveFinishEvent)
        {
            transform.DOMove(new Vector3(destX, destY, 0), _movementSpeed).SetEase(Ease.InOutCubic).onComplete =
                () =>
                {
                    _cell.UpdateCells(destX, destY);
                };
            if(moveFinishEvent)
            StartCoroutine(OnMoveFinish());
        }
        public void Move(Vector3 position, bool moveFinishEvent)
        {
            transform.DOMove(position, _movementSpeed).SetEase(Ease.InOutCubic).onComplete =
                () =>
                {
                    _cell.UpdateCells((int)position.x, (int)position.y);
                };
            if(moveFinishEvent)
            StartCoroutine(OnMoveFinish());
        }
        private IEnumerator OnMoveFinish()
        {
            yield return new WaitForSeconds(_movementSpeed);
            _onFinishMovingSubject.Raise(_cell);
        }
        [ContextMenu("Test move")]
        public void MoveTest()
        {
            Move(0, 0,false);
        }
    }
}

