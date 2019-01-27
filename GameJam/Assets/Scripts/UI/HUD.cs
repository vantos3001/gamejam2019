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

    public void SetHeartColor(Human.PartSettings PartState) { Heart.color = GetOrganDamageColor(PartState); }
    public void SetStomachColor(Human.PartSettings PartState) { Stomach.color = GetOrganDamageColor(PartState); }
    public void SetLiverColor(Human.PartSettings PartState) { Liver.color = GetOrganDamageColor(PartState); }
    public void SetLungsColor(Human.PartSettings PartState) { Lungs.color = GetOrganDamageColor(PartState); }

    public void SetKidneysColor(Human.PartSettings PartState1, Human.PartSettings PartState2) {
        float averageidneys = (PartState1.PartEatableObject.GetLeftToEatPercent() +
                PartState1.PartEatableObject.GetLeftToEatPercent()) / 2.0f;

        Kidneys.color = GetDamageColorByDamagePercent(averageidneys);
    }

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
    
    Color GetOrganDamageColor(Human.PartSettings PartState) {
        float AtePercent = PartState.PartEatableObject.GetAtePercent();

        float UIShownDamage = AtePercent / PartState.CriticalAtePercent;
        UIShownDamage = Mathf.Clamp(UIShownDamage, 0.0f, 1.0f);
        UIShownDamage = 1.0f - UIShownDamage;

        return GetDamageColorByDamagePercent(UIShownDamage);
    }

    Color GetDamageColorByDamagePercent(float LeftPercent) {
        if (LeftPercent > 0.9f) return GreenOrganColor;
        if (LeftPercent > 0.7f) return YellowOrganColor;
        if (LeftPercent > 0.3f) return OrangeOrganColor;
        if (LeftPercent > 0.0f) return RedOrganColor;
        return GreenOrganColor;
    }
}
