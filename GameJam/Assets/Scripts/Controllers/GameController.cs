using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Win,
    Lose,
    InProgress,
    None,
}

public class GameController : MonoBehaviour {
    //Fields
    //-Settings
    //
    public int StartScore = 200;
    public int WinScore = 1000;

    //--Links
    public UIManager _UIManager;
    
    public int EmptySpaceReduceScore = 1;
    public int BoneReduceScore = 1;
    
    private GameState _gameState = GameState.None;

    private event Action GameStateChanged;

    private void Awake() {
        InitGame();
        GameStateChanged += OnGameStateChanged;
    }

    public void InitGame() {
        if (SceneManager.GetActiveScene().name == "Worm Plus Meat Scene") {
            InitLevel();
        }
    }

    public void InitLevel() {        
        UpdateUIInfo();
    }

    private void CheckGameState() {
        var oldState = _gameState;
        
        //if (_currentScore <= 0 || _currentHumanHealth <= 0) {
        //    _gameState = GameState.Lose;
        //}else if (WinScore <= _currentScore) {
        //    _gameState = GameState.Win;
        //} else {
        //    _gameState = GameState.InProgress;
        //}

        //if (_gameState != oldState) {
        //    if (GameStateChanged != null) {
        //        GameStateChanged();
        //    }
        //}
    }

    private void OnGameStateChanged() {
        switch (_gameState) {
            case GameState.Win:
                _UIManager.ShowWinPanel();
                break;
            case GameState.Lose:
                _UIManager.ShowLosePanel();
                break;
            case GameState.InProgress:
                _UIManager.ShowHUD();
                break;
            default:
                Debug.LogError("Do not use " + _gameState + " state");
                break;
        }
    }

    private void UpdateUIInfo() {
        //_UIManager.SetScoreText(_currentScore);
        //_UIManager.SetHumanHealth(_currentHumanHealth);
    }
}
