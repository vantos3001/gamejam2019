using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Win,
    Lose,
    InProgress,
    None
}

public class GameController : MonoBehaviour {
    private static GameController _instance;

    public UIManager _UIManager;
    
    private const int MaxHumanHealth = 1000;

    private int _currentHumanHealth;
    
    private const int StartScore = 200;
    private const int WinScore = 1000;

    private int _currentScore;
    
    private GameState _gameState = GameState.None;

    private event Action GameStateChanged;

    private void Awake() {
        InitGame();
        GameStateChanged += OnGameStateChanged;
    }

    public void InitGame() {
    }

    public void InitLevel() {
        _currentHumanHealth = MaxHumanHealth;
        _currentScore = StartScore;
    }


    public void CheckWormAction(TextureSetupScript.EMapMaterial state) {
        switch (state) {
            case TextureSetupScript.EMapMaterial.Meat:
                EatMeat();
                break;
            case TextureSetupScript.EMapMaterial.Empty:
                MoveEmptySpace();
                break;
            case TextureSetupScript.EMapMaterial.Bone:
                EatBone();
                break;
            case TextureSetupScript.EMapMaterial.Brain:
                EatOrgan();
                break;
            default:
                Debug.Log("Do not handle " + state + " state");
                break;
        }
        
        CheckGameState();
    }
    
    private void EatMeat() {
    }

    private void MoveEmptySpace() {
        
    }

    private void EatBone() {
        
    }

    private void EatOrgan() {
        
    }

    private void ReduceHumanHealth(int points) {
        _currentHumanHealth -= points;
    }

    private void ReduceScore(int points) {
        _currentScore -= points;
    }

    private void AddScore(int points) {
        _currentScore += points;
    }

    private void CheckGameState() {
        _UIManager.OpenWinPanel();
        var oldState = _gameState;
        
        if (_currentScore <= 0 || _currentHumanHealth <= 0) {
            _gameState = GameState.Lose;
        }else if (WinScore <= _currentScore) {
            _gameState = GameState.Win;

        } else {
            _gameState = GameState.InProgress;
        }

        if (_gameState != oldState) {
            if (GameStateChanged != null) {
                GameStateChanged();
            }
        }
    }

    private void OnGameStateChanged() {
        switch (_gameState) {
            case GameState.Win:
                _UIManager.OpenWinPanel();
                break;
            case GameState.Lose:
                _UIManager.OpenLosePanel();
                break;
        }
    }
        

    public static GameController instance() {
        if (_instance == null) {
            _instance = new GameController();
        }
        return _instance;
    }
}
