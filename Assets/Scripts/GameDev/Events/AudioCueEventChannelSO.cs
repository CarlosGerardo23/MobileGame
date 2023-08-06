using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Audio;
namespace GameDev.Events
{
    [CreateAssetMenu(menuName = "Events/Sound")]
    public class AudioCueEventChannelSO : EventChannel
    {
        private OnPlayAudioCue _playAction;
        private OnStopAudioCue _stopAction;
        private OnFinishAction _onFinishAction;

        public void SubscribePlay(OnPlayAudioCue action) => _playAction += action;
        public void UnsubscribePlay(OnPlayAudioCue action) => _playAction -= action;
        public AudioCueKey RaisePlay(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration,Vector3 position) { return _playAction.Invoke(audioCue, audioConfiguration, position); }

        public void SubscribeStop(OnStopAudioCue action) => _stopAction += action;
        public void UnsubscribeStop(OnStopAudioCue action) => _stopAction -= action;
        public bool RaiseStop(AudioCueKey key) { return _stopAction.Invoke(key); }

        public void SubscribeFinish(OnFinishAction action) => _onFinishAction += action;
        public void UnsubscribeFinish(OnFinishAction action) => _onFinishAction -= action;
        public bool RaiseFinish(AudioCueKey audioCueKey) { return _onFinishAction.Invoke(audioCueKey); }

        public delegate AudioCueKey OnPlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 position);
        public delegate bool OnStopAudioCue(AudioCueKey key);
        public delegate bool OnFinishAction(AudioCueKey key);
    }
}
