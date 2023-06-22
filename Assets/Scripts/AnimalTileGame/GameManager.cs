using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDev.Behaviour2D.Controls;
using GameDev;
public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    [SerializeField] private AnimalMatchPlayerData _data;
    [SerializeField] private TimerController _timer;
    [SerializeField] private InputTouchControlsManager _inputs;

    private GameState _currentState;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        _currentState = GameState.IDLE;
        _data.ResetPints();
    }
    private void Update()
    {
        switch (_currentState)
        {
            case GameState.IDLE:
                break;
            case GameState.IN_GAME:
                _timer.StartTimer();
                _currentState = GameState.IDLE;
                break;
            case GameState.GAME_OVER:
                _inputs.SetInputsState(false);
                _currentState = GameState.IDLE;
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        _currentState = GameState.IN_GAME;
    }

    public void SetGameOver()
    {
        _currentState = GameState.GAME_OVER;
    }
}
public enum GameState { IDLE, IN_GAME, GAME_OVER };