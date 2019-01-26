﻿using System.Collections;
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
        var scoreText = Score.GetComponentInChildren<TextMeshProUGUI>();
        scoreText.text = ScorePrefix + score;
    }

    public void SetHumanHealthPerCent(int healthPerCent) {
        var healthText = HumanHealth.GetComponentInChildren<TextMeshProUGUI>();
        healthText.text = HumanHealthPrefix + healthPerCent + "%";
    }
}
