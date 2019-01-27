using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
    private const string HumanHealthPrefix = "Human health: ";

    public GameObject HumanHealth;

    public Slider ScoreBar;
    private MultipleIndicator _wormIndicator;

    public Slider HumanHealthBar;
    private MultipleIndicator _faceIndicator;
    private Image HumanHealthFillImage;

    private Color FillGreenColor;
    private Color FillRedColor;
    private Color FillOrangeColor;
    
    private float _minFlashValue = 0.9f;

    public float MaxScore;
    private float CurrentScore = 0;

    private void Start() {
        var scoreBarImages = ScoreBar.GetComponentsInChildren<Image>();
        foreach (var image in scoreBarImages) {
            if (image.name == "Worm Indicator") {
                _wormIndicator = image.GetComponent<MultipleIndicator>();
            }
        }

        var humanHealthImages = HumanHealthBar.GetComponentsInChildren<Image>();
        foreach (var image in humanHealthImages) {
            if (image.name == "Fill") {
                HumanHealthFillImage = image;
            }
            
            if (image.name == "Face Indicator") {
                _faceIndicator = image.GetComponent<MultipleIndicator>();
            }
        }
        
        FillGreenColor = new Color(0.24f,0.63f,0.28f);
        FillRedColor = new Color(0.8f,0.13f,0.15f);
        FillOrangeColor = new Color(0.88f,0.34f,0.15f);
    }

    public void SetHumanHealthPerCent(int healthPerCent) {
        var healthText = HumanHealth.GetComponentInChildren<TextMeshProUGUI>();
        healthText.text = HumanHealthPrefix + healthPerCent + "%";
    }

    public void SetCurrentScore(int score) {
        CurrentScore = score;

        ScoreBar.value = CalculateScore();
        _wormIndicator.CheckCurrentImage(ScoreBar.value);
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
    
    public void SetHumanHealth(float health) {
        HumanHealthBar.value = health;
        _faceIndicator.CheckCurrentImage(HumanHealthBar.value);
        
        if (HumanHealthBar.value > 0.5) {
            var value = Mathf.Lerp(0.5f, 1f, HumanHealthBar.value * 2 - 1);
            HumanHealthFillImage.color = Color.Lerp(FillOrangeColor, FillGreenColor, HumanHealthBar.value * 2 - 1);
        } else {
            var value = Mathf.Lerp(0f, 0.5f, HumanHealthBar.value * 2);
            HumanHealthFillImage.color = Color.Lerp(FillRedColor, FillOrangeColor, HumanHealthBar.value * 2);
        }

    }
}
