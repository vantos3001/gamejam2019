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

    private Color GreenOrganColor;
    private Color YellowOrganColor;
    private Color OrangeOrganColor;
    private Color RedOrganColor;
    private Color GreyOrganColor;

    private Image Stomach;
    private Image Lungs;
    private Image Liver;
    private Image Kidneys;
    private Image Heart;
    
    
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

            if (image.name == "Stomach") {
                Stomach = image;
            }
            
            if (image.name == "Lungs") {
                Lungs = image;
            }
            
            if (image.name == "Liver") {
                Liver = image;
            }
            
            if (image.name == "Kidneys") {
                Kidneys = image;
            }
            
            if (image.name == "Heart") {
                Heart = image;
            }
        }
        
        FillGreenColor = new Color(0.24f,0.63f,0.28f);
        FillRedColor = new Color(0.8f,0.13f,0.15f);
        FillOrangeColor = new Color(0.88f,0.34f,0.15f);
        
        RedOrganColor = new Color(0.82f,0.31f,0.15f);
        GreenOrganColor = new Color(0.17f,0.34f,0.16f);
        GreyOrganColor = new Color(0.36f, 0.34f, 0.31f);
        YellowOrganColor = new Color(0.57f,0.47f,0.18f);
        OrangeOrganColor = FillOrangeColor;
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

        HumanHealthFillImage.color = GetDamageColor(HumanHealthBar.value);
    }

    public void SetHeartColor(float healthOrgan) { Heart.color = GetOrganDamageColor(healthOrgan); }
    public void SetStomachColor(float healthOrgan) { Stomach.color = GetOrganDamageColor(healthOrgan); }
    public void SetKidneysColor(float healthOrgan) { Kidneys.color = GetOrganDamageColor(healthOrgan); }
    public void SetLiverColor(float healthOrgan) { Liver.color = GetOrganDamageColor(healthOrgan); }
    public void SetLungsColor(float healthOrgan) { Lungs.color = GetOrganDamageColor(healthOrgan); }
    
    Color GetDamageColor(float LeftPercent) {
        Color color;
        if (LeftPercent > 0.5) {
            var value = Mathf.Lerp(0.5f, 1f, LeftPercent * 2 - 1);
            color = Color.Lerp(FillOrangeColor, FillGreenColor, LeftPercent * 2 - 1);
        } else {
            var value = Mathf.Lerp(0f, 0.5f, LeftPercent * 2);
            color = Color.Lerp(FillRedColor, FillOrangeColor, LeftPercent * 2);
        }

        return color;
    }
    
    Color GetOrganDamageColor(float LeftPercent) {
        Color color;
        if (LeftPercent > 0.9f) {
            var value = Mathf.Lerp(0.9f, 1f, LeftPercent * 10 - 9);
            color = Color.Lerp(YellowOrganColor, GreenOrganColor, LeftPercent * 2 - 1);
            
        } else if (LeftPercent > 0.7f) {
            var value = Mathf.Lerp(0.7f, 0.9f, LeftPercent * 5 - 3.5f);
            color = Color.Lerp(OrangeOrganColor, YellowOrganColor, LeftPercent * 2 - 1);
            
        }else if (LeftPercent > 0.6f) {
            var value = Mathf.Lerp(0.6f, 0.7f, LeftPercent * 10 - 6);
            color = Color.Lerp(RedOrganColor, OrangeOrganColor, LeftPercent * 2 - 1);
            
        }else if (LeftPercent > 0.5f) {
            var value = Mathf.Lerp(0.5f, 0.6f, LeftPercent * 10 - 5);
            color = Color.Lerp(GreyOrganColor, RedOrganColor, LeftPercent * 2 - 1);
            
        } else {
            var value = Mathf.Lerp(0f, 0.5f, LeftPercent * 2);
            color = Color.Lerp(GreyOrganColor, GreyOrganColor, LeftPercent * 2 - 1);
        }

        return color;
    }
}
