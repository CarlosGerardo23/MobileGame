using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Events;
using GameDev.DesignPatterns;
namespace GameDev.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private SoundEventChannel _soundEventChannelObserver;
        [SerializeField] private SoundsObjectPooling _objectPooling;
        private int _currentKey;
        private List<SoundEmitter> _activatedSounds;
        private void OnEnable()
        {
            _soundEventChannelObserver.SubscribePlay(PlaySound);
            _soundEventChannelObserver.SubscribeStop(StopSound);
        }
        private void OnDisable()
        {
            _soundEventChannelObserver.UnsubscribePlay(PlaySound);
            _soundEventChannelObserver.UnsubscribeStop(StopSound);
        }

        private int PlaySound(AudioData data,Vector3 position)
        {
            SoundEmitter soundEmitter = _objectPooling.Request();
            soundEmitter.onStopSound += ReleaseObject;
            _activatedSounds.Add(soundEmitter);
            soundEmitter.Play(data,position);
            return _activatedSounds.Count;
        }

        private void StopSound(int key)
        {
            for (int i = 0; i < _activatedSounds.Count; i++)
            {
                if(_activatedSounds[i].Key==key)
                {
                    _activatedSounds[i].Stop();
                    return;
                }    
            }
        }

        private void ReleaseObject(SoundEmitter audioSound)
        {
            _objectPooling.ReleaseObject(audioSound);
        }
    }
}