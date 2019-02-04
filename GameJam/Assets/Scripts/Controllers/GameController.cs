using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState {
    Win,
    ReadyToWin,
    Lose,
    InProgress,
    None,
}

public class GameController : MonoBehaviour {
    //Fields
    //-Links
    public UIManager _UIManager;
    
    public int WinScore = 2000;
    
    private PlayerWormBehaviour _playerWormBehaviour = null;
    public Human Human = null;

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
        _playerWormBehaviour = FindObjectOfType<PlayerWormBehaviour>();
        Human = FindObjectOfType<Human>();
        
        LoadLevel();
    }

    private void LoadLevel() {
        GameState = GameState.InProgress;
    }

    private void FixedUpdate() {
        if (GameState == GameState.InProgress || GameState == GameState.ReadyToWin) {
            UpdateGameState();
        }
    }

    private void OnGameStateChanged() {
        switch (_gameState) {
            case GameState.Win:
                _UIManager.HideReadyToWinPanel();
                _UIManager.ShowWinPanel();
                break;
            case GameState.Lose:
                _UIManager.ShowLosePanel();
                var mediaController = FindObjectOfType<MediaController>();
                mediaController.mainTheme.Stop();
                mediaController.chewing.Stop();
                var gameOverSound = mediaController.gameOver;
                if(!gameOverSound.isPlaying) gameOverSound.PlayOneShot(gameOverSound.clip);
                StartCoroutine(RealoadGame());
                break;
            case GameState.InProgress:
                _UIManager.ShowHUD();
                _UIManager.HideReadyToWinPanel();
                break;
            case GameState.ReadyToWin:
                _UIManager.ShowReadyToWinPanel();
                break;
            default:
                Debug.LogError("Do not use " + _gameState + " state");
                break;
        }
    }
    
    IEnumerator RealoadGame()
    {
        yield return new WaitForSeconds(3);
        SwapSceneController.RestartGame();
    }
    

    public void UpdateGameState() {
        var score = GetCurrentScore();
        var humanState = GetHumanState();
        
        if (score <= 0 || humanState == Human.Damage.Death) {
            GameState = GameState.Lose;
        }

        if (score >= WinScore) {
            GameState = GameState.ReadyToWin;

            if (Input.GetKeyDown(KeyCode.E)) {
                GameState = GameState.Win;
            }
        } else {
            GameState = GameState.InProgress;
        }
    }

    public int GetCurrentScore() {
        return (int)_playerWormBehaviour.getPoints().getValue();
    }

    public int GetHumanPerCent() {
        var healthPerCent = (int)(Human.GetTotalPoints() * 100);
        return healthPerCent;
    }

    public float GetHumanHealth() {
        return Human.GetTotalPoints();
    }

    public Human.Damage GetHumanState() {
        return Human.GetTotalDamage();
    }

    private void StopGame(){
    }
}
