using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameDev.Audio
{
    [System.Serializable]
    public class AudioClipsGroup
    {
        #region Editor-Assigned
        [SerializeField] private SequenceMode _sequenceMode;
        [SerializeField] private AudioClip[] _audioClips;
        #endregion
        #region Properties
        public SequenceMode CurrentSequenceMode { get => _sequenceMode; set => _sequenceMode = value; }
        #endregion
        #region Private-Variablrs
        private int _nextClipToPlay = -1;
        private int _lastClipPlayed = -1;
        #endregion
        public AudioClip GetNextClip()
        {
            // Fast out if there is only one clip to play
            if (_audioClips.Length == 1)
                return _audioClips[0];

            if (_nextClipToPlay == -1)
            {
                // Index needs to be initialised: 0 if Sequential, random if otherwise
                _nextClipToPlay = (_sequenceMode == SequenceMode.SEQUENTIAL) ? 0 : UnityEngine.Random.Range(0, _audioClips.Length);
            }
            else
            {
                // Select next clip index based on the appropriate SequenceMode
                switch (_sequenceMode)
                {
                    case SequenceMode.RANDOM:
                        _nextClipToPlay = UnityEngine.Random.Range(0, _audioClips.Length);
                        break;

                    case SequenceMode.RANDOM_NO_IMMEDIATE_REPEATE:
                        do
                        {
                            _nextClipToPlay = UnityEngine.Random.Range(0, _audioClips.Length);
                        } while (_nextClipToPlay == _lastClipPlayed);
                        break;

                    case SequenceMode.SEQUENTIAL:
                        _nextClipToPlay = (int)Mathf.Repeat(++_nextClipToPlay, _audioClips.Length);
                        break;
                }
            }

            _lastClipPlayed = _nextClipToPlay;

            return _audioClips[_nextClipToPlay];
        }
    }
    public enum SequenceMode
    {
        RANDOM,
        RANDOM_NO_IMMEDIATE_REPEATE,
        SEQUENTIAL
    }
}