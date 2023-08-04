using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDev.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour
    {
        public Action<SoundEmitter> onStopSound;
        private AudioSource _audioSource;
        private int _key;

        public int Key => _key;
        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            
        }
        public void Play(AudioData data, Vector3 position)
        {
            transform.position = position;
            _audioSource.playOnAwake = false;
            _audioSource.clip = data.Clip;
            _audioSource.volume = data.Volume;
            _audioSource.spatialBlend = data.SpatialBlend;
            _audioSource.loop = data.HasToLoop;
            _audioSource.Play();

            if(!data.HasToLoop)
            {
                StartCoroutine(OnFinishSound(_audioSource.clip.length));
            }
        }
        public void Stop()
        {
            
        }
        private IEnumerator OnFinishSound(float musicDuration)
        {
            yield return new WaitForSeconds(musicDuration);
            onStopSound(this);
        }
    }
}
