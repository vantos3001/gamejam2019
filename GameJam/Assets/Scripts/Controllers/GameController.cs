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
    private static GameController _instance;

    public UIManager _UIManager;
    
    private const int MaxHumanHealth = 1000;

    private int _currentHumanHealth;
    
    public int StartScore = 200;
    public  int WinScore = 1000;

    private int _currentScore;

    public int MeatReduceHumanHealth = 1;
    public int OrganReduceHumanHealth = 1;
    public int BoneReduceHumanHealth = 1;

    public int EmptySpaceReduceScore = 1;
    public int BoneReduceScore = 1;

    public int MeatAddScore = 1;
    public int OrganAddScore = 1;
    
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
        _currentHumanHealth = MaxHumanHealth;
        _currentScore = StartScore;
        
        UpdateUIInfo();
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
        
        UpdateUIInfo();
        CheckGameState();
    }
    
    private void EatMeat() {
        AddScore(MeatAddScore);
        ReduceHumanHealth(MeatReduceHumanHealth);
    }

    private void MoveEmptySpace() {
        ReduceScore(EmptySpaceReduceScore);
        
    }

    private void EatBone() {
        ReduceScore(BoneReduceScore);
        ReduceHumanHealth(BoneReduceHumanHealth);
        
    }

    private void EatOrgan() {
        AddScore(OrganAddScore);
        ReduceHumanHealth(OrganReduceHumanHealth);
        
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
        _UIManager.SetScoreText(_currentScore);
        _UIManager.SetHumanHealth(_currentHumanHealth);
    }
        

    public static GameController instance() {
        if (_instance == null) {
            _instance = new GameController();
        }
        return _instance;
    }
}
