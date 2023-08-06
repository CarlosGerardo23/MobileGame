using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameDev.Audio
{
    [CreateAssetMenu(menuName = "Audio/AduioCue")]
    public class AudioCueSO : ScriptableObject
    {
        public bool looping = false;
        [SerializeField] private AudioClipsGroup[] _audioClipGroups = default;
        public AudioClip[] GetClips()
        {
            int numberOfClips = _audioClipGroups.Length;
            AudioClip[] resultingClips = new AudioClip[numberOfClips];

            for (int i = 0; i < numberOfClips; i++)
            {
                resultingClips[i] = _audioClipGroups[i].GetNextClip();
            }

            return resultingClips;
        }
    }
}