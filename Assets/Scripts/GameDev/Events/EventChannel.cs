using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventChannel : ScriptableObject
{
    [TextArea(5, 6)]
    [SerializeField] private string _eventContext;
}
