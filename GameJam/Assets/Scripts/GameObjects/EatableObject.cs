using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableObject : MonoBehaviour
{
    public enum EEatableObjectType
    {
        Meat,
        Heart,
        Stomach,
        Bone
    }
    
    //Fields
    //-Settings
    public SpriteRenderer Visual = null;
    public EEatableObjectType ObjectType = EEatableObjectType.Meat;

    public float EatingBoardInUnits = 0.3f;
    public Color EatingBoardColor = new Color(1.0f, 0.0f, 0.0f, 0.5f);

    //--Cached at start
    private int _startingPixelsCount = 0;
    private int _leftPixelsCount = 0;
    
    //-Runtime
    private Texture2D _visualTexture = null;
    private Texture2D _logicTexture = null;
    
    //Methods
    //-Initialization
    void Start() {
        if (!Visual){
            Debug.Log("NO VISUAL RENDER FOR EATABLE OBJECT!");
            return;
        }

        Texture2D theTexture = (Texture2D)Instantiate(Visual.sprite.texture);
        
        _visualTexture = new Texture2D(theTexture.width, theTexture.height, TextureFormat.RGBA32, false);
        Color[] Pixels = theTexture.GetPixels();
        _visualTexture.SetPixels(Pixels);
        _visualTexture.Apply();
        MaterialPropertyBlock theBlock = new MaterialPropertyBlock();
        theBlock.SetTexture(Shader.PropertyToID("_MainTex"), _visualTexture);
        Visual.SetPropertyBlock(theBlock);
        
        _logicTexture = new Texture2D(_visualTexture.width, _visualTexture.height, TextureFormat.R8, false);
        Color[] theVisualPixelColors = _visualTexture.GetPixels();
        Color[] theLogicPixelColors = new Color[theVisualPixelColors.Length];
        for (int theColorIndex = 0; theColorIndex < theVisualPixelColors.Length; ++theColorIndex){
            theLogicPixelColors[theColorIndex].r = (0.0f == theVisualPixelColors[theColorIndex].a) ? 0.0f : 1.0f;
        }
        _logicTexture.SetPixels(theLogicPixelColors);
        _logicTexture.Apply();
        
        _startingPixelsCount = theLogicPixelColors.Length;
        _leftPixelsCount = _startingPixelsCount;
    }

    //-Life access
    public float GetLeftToEatPercent() { return ((float)_leftPixelsCount) / _startingPixelsCount; }
    public float GetAtePercent() { return 1.0f - GetLeftToEatPercent(); }
    
    //-Eatable World API
    public void EatInCircle(Vector2 WorldPosition, float WorldRadius) {
        int Radius = (int)ConvertValueInUnitsToValueInPixels(WorldRadius);

        int EatingBoardInPixels = (int)ConvertValueInUnitsToValueInPixels(EatingBoardInUnits);

        Vector2 PixelPosition = ConvertWorldPositionToPixelPosition(WorldPosition);
        int CenterX = (int)PixelPosition.x;
        int CenterY = (int)PixelPosition.y;
        
        int theStartX = (int)Mathf.Clamp(CenterX - Radius, 0.0f,  _logicTexture.width);
        int theEndX = (int)Mathf.Clamp(CenterX + Radius, 0.0f,  _logicTexture.width);
        
        int theStartY = (int)Mathf.Clamp(CenterY - Radius, 0.0f,  _logicTexture.height);
        int theEndY = (int)Mathf.Clamp(CenterY + Radius, 0.0f,  _logicTexture.height);
                
        for (int theY = theStartY; theY < theEndY; ++theY) {
            int DeltaY = theY - CenterY;
            for (int theX = theStartX; theX < theEndX; ++theX) {
                int DeltaX = theX - CenterX;
                
                int PixelDistanceToCenter = (int)Mathf.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY);
                if (PixelDistanceToCenter > Radius) continue;                

                Color theLogicPixelColor = _logicTexture.GetPixel(theX, theY);
                if (0.0f != theLogicPixelColor.r) {
                    _logicTexture.SetPixel(theX, theY, new Color(0.0f, 0.0f, 0.0f, 0.0f));
                    --_leftPixelsCount;
                }

                /*
                Color theVisualPixelColor = _visualTexture.GetPixel(theX, theY);

                //0 is for color, 1 is for eat color
                float theColorLerpTNotClamped =
                        ((float)Radius - (float)PixelDistanceToCenter) / (float)EatingBoardInPixels;
                float theColorLerpT = Mathf.Clamp(theColorLerpTNotClamped, 0.0f, 1.0f);

                Color theNewColor;
                if (theColorLerpTNotClamped < 1.0f) {
                    theNewColor = Color.Lerp(theVisualPixelColor, EatingBoardColor, theColorLerpT);
                } else {
                    theNewColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
                }
                
                if (theVisualPixelColor.a > theNewColor.a) {}
                theVisualPixelColor = theVisualPixelColor.a > theNewColor.a;
                */
                _visualTexture.SetPixel(theX, theY, new Color(0.0f, 0.0f, 0.0f, 0.0f));
            }
        }
        
        _logicTexture.Apply();
        _visualTexture.Apply();
    }
    
    public float GetObjectPercentInCircle(Vector2 WorldPosition, float WorldRadius) {
        int Radius = (int)ConvertValueInUnitsToValueInPixels(WorldRadius);

        Vector2 PixelPosition = ConvertWorldPositionToPixelPosition(WorldPosition);
        int CenterX = (int)PixelPosition.x;
        int CenterY = (int)PixelPosition.y;
        
        int theStartX = (int)Mathf.Clamp(CenterX - Radius, 0.0f,  _logicTexture.width);
        int theEndX = (int)Mathf.Clamp(CenterX + Radius, 0.0f,  _logicTexture.width);
        
        int theStartY = (int)Mathf.Clamp(CenterY - Radius, 0.0f,  _logicTexture.height);
        int theEndY = (int)Mathf.Clamp(CenterY + Radius, 0.0f,  _logicTexture.height);

        int ObjectPixlesCount = 0;
        
        for (int theY = theStartY; theY < theEndY; ++theY) {
            int DeltaY = theY - CenterY;
            for (int theX = theStartX; theX < theEndX; ++theX) {
                int DeltaX = theX - CenterX;
                
                int PixelDistanceToCenter = (int)Mathf.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY);
                if (PixelDistanceToCenter > Radius) continue;                
                
                Color PixelColor = _logicTexture.GetPixel(theX, theY);
                ObjectPixlesCount += (0.0f == PixelColor.r) ? 0 : 1;
            }
        }
        
        float PixelsInCircle = (float)(Math.PI * Radius*Radius);
        return ObjectPixlesCount/PixelsInCircle;
    }

    //-Utils
    private float ConvertValueInUnitsToValueInPixels(float ValueInUnits) {
        float thePixelSizeInUnits = transform.localScale.x / _logicTexture.width;
        return ValueInUnits / thePixelSizeInUnits;
    }

    private Vector2 ConvertWorldPositionToPixelPosition(Vector2 WorldPosition) {
        Vector3 LocalPosition = transform.InverseTransformPoint(new Vector3(WorldPosition.x, WorldPosition.y, 0.0f));

        float AspectRatio = (float)_logicTexture.width / _logicTexture.height;
        
        float thePixelPositionX = _logicTexture.width * (0.5f + LocalPosition.x);
        float thePixelPositionY = _logicTexture.height * (0.5f + LocalPosition.y * AspectRatio);

        return new Vector2(thePixelPositionX, thePixelPositionY);
    }
}
