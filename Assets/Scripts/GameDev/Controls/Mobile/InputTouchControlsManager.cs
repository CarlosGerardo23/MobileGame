using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameDev.Behaviour2D.Controls
{
    public class InputTouchControlsManager : MonoBehaviour
    {
        [SerializeField] private InputTouchControls _inputTouchControls;
        private void OnEnable()
        {
            _inputTouchControls.Enable();
        }
        private void OnDisable()
        {
            _inputTouchControls.Disable();

        }

        public void SetInputsState(bool state)
        {
            if (state)
                _inputTouchControls.Enable();
            else
                _inputTouchControls.Disable();
        }
    }
}

