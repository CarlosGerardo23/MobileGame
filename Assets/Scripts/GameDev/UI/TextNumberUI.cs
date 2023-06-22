using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Events;
namespace GameDev.UI
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class TextNumberUI : MonoBehaviour
    {
        [SerializeField] private FloatEventChannel _updateNumberObserver;
        [SerializeField] private float _addFactor;
        [SerializeField] private bool _useDealy;
        [SerializeField] private float _timeToDelay;
        private TMPro.TextMeshProUGUI _text;
        private float _displayNumber;
        private void Awake()
        {
            _displayNumber = 0;
            _text = GetComponent<TMPro.TextMeshProUGUI>();
        }
        private void OnEnable()
        {
            _updateNumberObserver.Subscribe(UpdateNumber);
        }
        private void OnDisable()
        {
            _updateNumberObserver.Unsubscribe(UpdateNumber);
        }
        private void UpdateNumber(float number)
        {
            if (_useDealy)
                StartCoroutine(UpdateNumberDelay(number));
            else
            {
                _displayNumber = number;
                _text.text = _displayNumber.ToString();
            }
        }
        private IEnumerator UpdateNumberDelay(float number)
        {
            while (_displayNumber <= number)
            {
                _displayNumber += _addFactor;
                _text.text = _displayNumber.ToString();
                yield return new WaitForSeconds(_timeToDelay);
            }
        }
    }
}
