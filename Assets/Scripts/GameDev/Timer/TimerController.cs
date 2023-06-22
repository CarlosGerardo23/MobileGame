using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using GameDev.Events;
using UnityEngine;
namespace GameDev
{
    public class TimerController : MonoBehaviour
    {
        [SerializeField] private float _time;
        [SerializeField] private UnityEvent _onTimerEnd;
        [SerializeField] private FloatEventChannel _sendCurrentTimeEventChannelSubject;
        private bool _isTicking;
        private float _currenTime;

        private void Update()
        {
            if (_isTicking)
            {
                _currenTime += Time.deltaTime;
                _sendCurrentTimeEventChannelSubject?.Raise(_currenTime);
                if (_currenTime >= _time)
                {
                    _isTicking = false;
                    _currenTime = 0;
                    _onTimerEnd?.Invoke();               
                }
            }
        }
        public void StartTimer()
        {
            _isTicking = true;
        }

        public void ResetTimer()
        {
            _currenTime = 0;
        }
    }
}
