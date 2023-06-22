using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using GameDev.Events;
namespace GameDev.Ui
{
    [RequireComponent(typeof(Slider))]
    public class TimerBarUI : MonoBehaviour
    {
        [SerializeField] private Image _sliderImage;
        [SerializeField] private Gradient _gradient;
        [SerializeField] private FloatEventChannel _getTimerEventChannelObserver;
        [SerializeField] private SliderMovement _currentState;
        [SerializeField] private Vector2 _oldFactor;
        private Slider _slider;
        private float _currentFactor;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }
        private void OnEnable()
        {
            _getTimerEventChannelObserver.Subscribe(UpdateSlider);
        }
        private void OnDisable()
        {
            _getTimerEventChannelObserver.Unsubscribe(UpdateSlider);
        }
        private void UpdateSlider(float factor)
        {
            if (_currentState == SliderMovement.MAX_MIN)
            {
                _currentFactor = GameDev.DevMath.ConvertRange(_oldFactor.x, _oldFactor.y, _slider.minValue, _slider.maxValue, factor);
                print($"fatcor is {factor}, new factor is {_currentFactor}");
                _slider.value = _currentFactor;
                _sliderImage.color = _gradient.Evaluate(_currentFactor);
            }
            else
            {
                _currentFactor = GameDev.DevMath.ConvertRange(_oldFactor.x, _oldFactor.y, _slider.minValue, _slider.maxValue, factor);
                print($"fatcor is {factor}, new factor is {_currentFactor}");
                _currentFactor = _slider.maxValue - _currentFactor;
               

                _slider.value = _currentFactor;
                _sliderImage.color = _gradient.Evaluate(_currentFactor);
            }
        }
    }
    public enum SliderMovement { MIN_MAX, MAX_MIN };
}
