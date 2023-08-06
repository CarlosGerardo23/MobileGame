using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.DesignPatterns;
namespace GameDev.Audio
{
    [System.Serializable]
    public class SoundsObjectPooling : ObjectPooling<SoundEmitter>
    {
       private SoundEmitter _prefab;
        public SoundsObjectPooling(SoundEmitter prefab)
        {
            _prefab = prefab;
            Init();
        }
        protected override SoundEmitter InstanceObject()
        {

            return GameObject.Instantiate(_prefab);
        }


    }
}