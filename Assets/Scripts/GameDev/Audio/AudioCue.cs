using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Events;
namespace GameDev.Audio
{
    public class AudioCue : MonoBehaviour
    {
        [SerializeField] AudioData _data;
        [SerializeField] SoundEventChannel _soundManagerEvent;
        private int _key = -1;
        public void Play()
        {
            _key = _soundManagerEvent.RaisePlay(_data, transform.position);
        }

        public void Stop()
        {
            if(_key==-1)
            {
                print("Try to sotp a sfs or music that has not index");
                return;
            }
            _soundManagerEvent.RaiseStop(_key);
            _key = -1;
        }
    }
}
