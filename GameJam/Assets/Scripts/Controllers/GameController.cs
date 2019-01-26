using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Win,
    Lose,
    InProgress,
}

public class GameController : MonoBehaviour {
    private const int MaxHumanHealth = 1000;

    private int _currentHumanHealth;
    
    private int _startScore = 200;
    private int _winScore = 1000;

    private int _currentScore;
    
    public void InitGame(){}
    
    public void InitLevel(){}


    public void CheckWormAction(TextureSetupScript.EMapMaterial state) {
        //realize EMapMaterial state
        
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
        
    }
}
