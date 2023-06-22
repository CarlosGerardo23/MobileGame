using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Events;

[CreateAssetMenu(menuName ="Data/Player")]
public class AnimalMatchPlayerData : ScriptableObject
{
    [SerializeField] private FloatEventChannel _onUpdatePointsSubject;
    private int _points;
    public int Points => _points;
    public void ResetPints()
    {
        _points = 0;
    }
    public void UpdatePoints(int points)
    {
        _points += points;
        _onUpdatePointsSubject?.Raise(_points);
    }
}
