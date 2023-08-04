using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameDev.Audio
{
    [System.Serializable]
    public class AudioData
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private bool _hasToLoop;
        [Range(0, 1f)]
        [SerializeField] private float _volume;
        [Range(0, 1f)]
        [SerializeField] private float _spatialBlend;

        public AudioClip Clip => _clip;
        public bool HasToLoop => _hasToLoop;
        public float Volume => _volume;
        public float SpatialBlend => _spatialBlend;
    }
}
