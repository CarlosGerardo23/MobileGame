using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Audio;
using static GameDev.Events.SoundEventChannel;

namespace GameDev.Events
{
    [CreateAssetMenu(menuName = "Events/Sound")]
    public class SoundEventChannel : ScriptableObject
    {
        private OnPlayAudioCue _playAction;
        private OnStopAudioCue _stopAction;
        public void SubscribePlay(OnPlayAudioCue action) => _playAction += action;
        public void UnsubscribePlay(OnPlayAudioCue action) => _playAction -= action;
        public int RaisePlay(AudioData data, Vector3 position) { return _playAction.Invoke(data, position); }
        public void SubscribeStop(OnStopAudioCue action) => _stopAction += action;
        public void UnsubscribeStop(OnStopAudioCue action) => _stopAction -= action;
        public void RaiseStop(int key) => _stopAction?.Invoke(key);

        public delegate int OnPlayAudioCue(AudioData data, Vector3 position);
        public delegate void OnStopAudioCue(int key);
    }
}
