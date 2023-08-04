using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.DesignPatterns;
namespace GameDev.Audio
{
    public class SoundsObjectPooling : ObjectPooling<SoundEmitter>
    {
        [SerializeField] private SoundEmitter _prefab;
        protected override SoundEmitter InstanceObject()
        {
            return GameObject.Instantiate(_prefab);
        }

       
    }
}