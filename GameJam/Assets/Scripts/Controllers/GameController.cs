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
    private const int MaxHumanHealth = 1000;

    private int _currentHumanHealth;
    
    private int _startScore = 200;
    private int _winScore = 1000;

    private int _currentScore;

    private GameState _gameState = GameState.None;
    
    public void InitGame(){}
    
    public void InitLevel(){}


    public void CheckWormAction(TextureSetupScript.EMapMaterial state) {
        //realize EMapMaterial state
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
        if (_currentScore <= 0 || _currentHumanHealth <= 0) {
            _gameState = GameState.Lose;
        }else if (_winScore <= _currentScore) {
            _gameState = GameState.Win;

        } else {
            _gameState = GameState.InProgress;
        }
    }
}
