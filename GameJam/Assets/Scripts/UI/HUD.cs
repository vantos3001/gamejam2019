using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    private const string ScorePrefix = "Score: ";
    private const string HumanHealthPrefix = "Human health: ";
    
    public GameObject Score;
    
    public GameObject HumanHealth;

    public Slider ScoreBar;
    private Image FillImage;

    public float MaxScore;
    private float CurrentScore = 0;

    private void Start() {
        var scoreBarImages = ScoreBar.GetComponentsInChildren<Image>();
        foreach (var image in scoreBarImages) {
            if (image.name == "Fill") {
                FillImage = image;
            }
        }
    }

    public void SetScoreText(int score) {
        var scoreText = Score.GetComponentInChildren<TextMeshProUGUI>();
        scoreText.text = ScorePrefix + score;
    }

    public void SetHumanHealthPerCent(int healthPerCent) {
        var healthText = HumanHealth.GetComponentInChildren<TextMeshProUGUI>();
        healthText.text = HumanHealthPrefix + healthPerCent + "%";
    }

    public void SetCurrentScore(int score) {
        CurrentScore = score;

        ScoreBar.value = CalculateScore();
        FillImage.color = Color.Lerp(Color.red, Color.green, ScoreBar.value);
    }

    private float CalculateScore() {
        return CurrentScore / MaxScore;
    }
}
