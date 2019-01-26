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
    //-Links
    public UIManager _UIManager;
    
    private GameState _gameState = GameState.None;

    private void Awake() {
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
