using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Events;
namespace GameDev.Audio
{
    public class AudioCue : MonoBehaviour
    {
        [Header("Sound definition")]
        [SerializeField] private AudioCueSO _audioCue = default;
        [SerializeField] private bool _playOnStart = false;

        [Header("Configuration")]
        [SerializeField] private AudioCueEventChannelSO _audioCueEventChannel = default;
        [SerializeField] private AudioConfigurationSO _audioConfiguration = default;


        private AudioCueKey controlKey = AudioCueKey.Invalid;

        private void Start()
        {
            if (_playOnStart)
                StartCoroutine(PlayDelayed());
        }

        private void OnDisable()
        {
            _playOnStart = false;
            StopAudioCue();
        }

        private IEnumerator PlayDelayed()
        {
            yield return new WaitForSeconds(1f);
            if (_playOnStart)
                Play();
        }
        public void Play()
        {
            controlKey  = _audioCueEventChannel.RaisePlay(_audioCue, _audioConfiguration, transform.position);
        }

        public void Play(Vector3 position)
        {
            controlKey = _audioCueEventChannel.RaisePlay(_audioCue, _audioConfiguration, position);

        }

        public void StopAudioCue()
        {
            if (controlKey != AudioCueKey.Invalid)
            {
                if (!_audioCueEventChannel.RaiseStop(controlKey))
                {
                    controlKey = AudioCueKey.Invalid;
                }
            }
        }
        public void FinishAudioCue()
        {
            if (controlKey != AudioCueKey.Invalid)
            {
                if (!_audioCueEventChannel.RaiseFinish(controlKey))
                {
                    controlKey = AudioCueKey.Invalid;
                }
            }
        }
    }
}
