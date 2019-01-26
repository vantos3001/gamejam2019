﻿using System;
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
    
    public int WinScore = 3000;
    
    private ClearController _playerClearController = null;
    private Human _human = null;

    private GameState _gameState = GameState.None;

    private GameState GameState {
        get { return _gameState; }
        set {
            if (_gameState == value) {
                return;
            }

            _gameState = value;
            OnGameStateChanged();
        }
    }

    private void Start() {
        _playerClearController = FindObjectOfType<ClearController>();
        _human = FindObjectOfType<Human>();
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

    public void CheckGameState(int score, Human.Damage humanState) {
        if (score <= 0 || humanState == Human.Damage.Death) {
            GameState = GameState.Lose;
        }

        if (score >= WinScore) {
            GameState = GameState.Win;
        }
    }

    public int GetCurrentScore() {
        return (int) _playerClearController.GetCurrentPoints();
    }

    public int GetHumanPerCent() {
        var healthPerCent = (int)(_human.GetTotalPoints() * 100);
        return healthPerCent;
    }

    private void UpdateUIInfo() {
        //_UIManager.SetScoreText(_currentScore);
        //_UIManager.SetHumanHealth(_currentHumanHealth);
    }
}
