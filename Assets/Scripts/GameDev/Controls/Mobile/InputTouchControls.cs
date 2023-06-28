using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine;
using System;

using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using Unity.VisualScripting.FullSerializer;

namespace GameDev.Behaviour2D.Controls
{
    [CreateAssetMenu(menuName = "Inputs/TocuhControls")]
    public class InputTouchControls : ScriptableObject
    {
        public Action<Vector2> OnStartedTouchPosition;
        public Action<Vector2> OnEndedTouchPosition;

        public void Enable()
        {
            EnhancedTouch.TouchSimulation.Enable();
            EnhancedTouch.EnhancedTouchSupport.Enable();
            EnhancedTouch.Touch.onFingerDown += OnFingerDown;
            EnhancedTouch.Touch.onFingerUp += OnFingerUp;
        }
        public void Disable()
        {
            EnhancedTouch.Touch.onFingerDown -= OnFingerDown;
            EnhancedTouch.Touch.onFingerUp -= OnFingerUp;
            EnhancedTouch.TouchSimulation.Disable();
            EnhancedTouch.EnhancedTouchSupport.Disable();
        }
        private void OnFingerUp(Finger finger)
        {
            if (OnEndedTouchPosition != null)
            {
                OnEndedTouchPosition(finger.currentTouch.screenPosition);
//                Debug.Log($"{finger.currentTouch.screenPosition}");
            }

        }

        private void OnFingerDown(Finger finger)
        {

            if (OnStartedTouchPosition != null)
            {
                OnStartedTouchPosition(finger.currentTouch.screenPosition);
           //     Debug.Log($"{finger.currentTouch.screenPosition}");

            }

        }



    }
}
