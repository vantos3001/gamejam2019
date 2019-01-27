using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleIndicator : MonoBehaviour {

    public Image TargetImage;
    
    public List<Sprite> Sprites;

    private int currentImageIndex;

    private int SpriteCount {
        get { return Sprites.Count; }
    }

    public void CheckCurrentImage(float value) {
        var newImageIndex = CalculateIndex(value);

        if (newImageIndex != currentImageIndex) {
            currentImageIndex = newImageIndex;
            TargetImage.sprite = Sprites[newImageIndex];
        }
    }

    private int CalculateIndex(float value) {
        var index = (int)Math.Round(((SpriteCount - 1) * value), MidpointRounding.AwayFromZero);
        return index;
    }
    
    
}
