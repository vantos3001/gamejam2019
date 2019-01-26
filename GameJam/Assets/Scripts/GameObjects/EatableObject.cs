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
    }

    //-Eatable World API
    public void EatInCircle(Vector2 WorldPosition, float WorldRadius) {
        int Radius = (int)ConvertValueInUnitsToValueInPixels(WorldRadius);
        
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
                
                _logicTexture.SetPixel(theX, theY, new Color(0.0f, 0.0f, 0.0f, 0.0f));

                Color theVisualPixelColor = _visualTexture.GetPixel(theX, theY);
                theVisualPixelColor.a = 0.0f;
                _visualTexture.SetPixel(theX, theY,  theVisualPixelColor);
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
        int theWidth = _logicTexture.width;
        int theHeight = _logicTexture.height;

        float theScaleX = transform.localScale.x;
        float theScaleY = transform.localScale.y;

        if (theScaleX/theScaleY != _logicTexture.width/_logicTexture.height) {
            Debug.Log("!!! INCORRECT MAP OBJECT ASPECT RATIO !!!");
        }

        //Here we have vector in local space
        // [-0.5 , -0.5] -> bottom left
        // [ 0.5 ,  0.5] -> top right
        Vector3 LocalPosition = transform.InverseTransformPoint(new Vector3(WorldPosition.x, WorldPosition.y, 0.0f));

        float thePixelPositionX = _logicTexture.width * (0.5f + LocalPosition.x);
        float thePixelPositionY = _logicTexture.height * (0.5f + LocalPosition.y);

        return new Vector2(thePixelPositionX, thePixelPositionY);
    }
}
