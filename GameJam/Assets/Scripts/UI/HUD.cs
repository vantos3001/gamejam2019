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

    private Color FlashStartColor; 
    
    public GameObject HumanHealth;

    public Slider ScoreBar;
    private Image FillImage;
    private Image FlashImage;
    private WormIndicator WormIndicator;
    
    private float _minFlashValue = 0.9f;

    public float MaxScore;
    private float CurrentScore = 0;

    private void Start() {
        var scoreBarImages = ScoreBar.GetComponentsInChildren<Image>();
        foreach (var image in scoreBarImages) {
            if (image.name == "Fill") {
                FillImage = image;
            }

            if (image.name == "Flash") {
                FlashImage = image;
                FlashImage.gameObject.SetActive(false);
            }
            
            if (image.name == "Worm Indicator") {
                WormIndicator = image.GetComponent<WormIndicator>();
            }
        }

        var flashColor = FlashImage.color;
        FlashStartColor = new Color(flashColor.r, flashColor.g, flashColor.b, 0);
    }

    public void SetHumanHealthPerCent(int healthPerCent) {
        var healthText = HumanHealth.GetComponentInChildren<TextMeshProUGUI>();
        healthText.text = HumanHealthPrefix + healthPerCent + "%";
    }

    public void SetCurrentScore(int score) {
        CurrentScore = score;

        ScoreBar.value = CalculateScore();
        WormIndicator.CheckCurrentImage(ScoreBar.value);
//        FillImage.color = Color.Lerp(Color.red, Color.green, ScoreBar.value);

//        if (ScoreBar.value < _minFlashValue) {
//            FlashImage.gameObject.SetActive(false);
//            FlashImage.color = FlashStartColor;
//        } else {
//            FlashImage.gameObject.SetActive(true);
//            if (FlashImage.color == FlashStartColor) {
//                FlashImage.color = new Color(FlashStartColor.r,FlashStartColor.g,FlashStartColor.b, 0.5f);
//            } else {
//                FlashImage.color = FlashStartColor;
//            }
//        }
    }

    private float CalculateScore() {
        return CurrentScore / MaxScore;
    }
}
