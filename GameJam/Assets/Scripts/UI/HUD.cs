using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour {
    private const string ScorePrefix = "Score: ";
    private const string HumanHealthPrefix = "Human health: ";
    
    public GameObject Score;
    
    public GameObject HumanHealth;


    public void SetScoreText(int score) {
        var scoreText = Score.GetComponent<TextMeshPro>();
        scoreText.text = ScorePrefix + score;
    }

    public void SetHumanHealth(int health) {
        var healthText = HumanHealth.GetComponent<TextMeshPro>();
        healthText.text = HumanHealthPrefix + health;
    }
}
