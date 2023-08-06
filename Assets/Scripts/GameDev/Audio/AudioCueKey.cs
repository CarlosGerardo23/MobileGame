using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDev.Audio
{
    public struct AudioCueKey
    {
        public static AudioCueKey Invalid = new AudioCueKey(-1, null);
        public int Value;
        public AudioCueSO AudioCue;

        public AudioCueKey(int key, AudioCueSO audioCue)
        {
            Value = key;
            AudioCue = audioCue;
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ AudioCue.GetHashCode();
        }
        public static bool operator == (AudioCueKey x, AudioCueKey y)
        {
            return x.Value == y.Value && x.AudioCue == y.AudioCue;
        }
        public static bool operator !=(AudioCueKey x, AudioCueKey y)
        {
            return !(x == y);
        }
    }
}