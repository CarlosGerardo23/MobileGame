using GameDev.Behaviour2D.Puzzle.Board;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDev.Behaviour2D.Puzzle.Camera
{
    public class BoardCameraManager : MonoBehaviour
    {
        [SerializeField] private BoardController _BoardController;
        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetOrthographic;

        [SerializeField] private float _zoomCamera;
        [SerializeField] private float _offsetVerticalCamera;

        private UnityEngine.Camera cameraReference;

        private void Awake()
        {
            cameraReference = UnityEngine.Camera.main;
        }
        void Start()
        {
            SetCameraPosition();
            SetOrthographicValue();
        }

        private void SetCameraPosition()
        {
            float newPosX = (_BoardController.BoardDimension.x / 2) - _offsetX;
            float newPosY = (_BoardController.BoardDimension.y / 2) - _offsetY + _offsetVerticalCamera;
            cameraReference.transform.position = new Vector3(newPosX, newPosY, -10);
        }

        private void SetOrthographicValue()
        {
            float horizontal = _BoardController.BoardDimension.x + _offsetOrthographic;
            float vertical = (_BoardController.BoardDimension.y / 2) + _offsetOrthographic;
            cameraReference.orthographicSize = horizontal > vertical ? horizontal + _zoomCamera : vertical;
        }


    }
}

