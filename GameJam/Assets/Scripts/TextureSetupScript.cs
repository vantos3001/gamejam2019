using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TextureSetupScript : MonoBehaviour {
    public enum EMapMaterial {
        Meat,
        Bone,
        Brain,
        Empty,
        NONE
    };

    private Color GetColorForMaterial(EMapMaterial InMaterial) {
        switch (InMaterial) {
            case EMapMaterial.Meat: return new Color(0.1f, 1.0f, 0.0f, 1.0f);
            case EMapMaterial.Bone: return new Color(0.2f, 0.0f, 1.0f, 1.0f);
            case EMapMaterial.Brain: return new Color(0.3f, 1.0f, 1.0f, 1.0f);
            case EMapMaterial.Empty: return new Color(0.4f, 0.0f, 0.0f, 0.0f);
        }

        return new Color();
    }

    EMapMaterial GetMaterialForColor(Color InColor) {
        int ColorId = (int)Math.Round(InColor.r * 10.0f);

        switch (ColorId) {
            case 1: return EMapMaterial.Meat;
            case 2: return EMapMaterial.Bone;
            case 3: return EMapMaterial.Brain;
            case 4: return EMapMaterial.Empty;
        }

        return EMapMaterial.NONE;
    }

    Texture2D _texture = null;

    void Start() {
        SpriteRenderer theSpriteRender = gameObject.GetComponent<SpriteRenderer>();

        Texture2D mapTexture = (Texture2D)Instantiate(Resources.Load("meat1"));
        _texture = new Texture2D(mapTexture.width, mapTexture.height, TextureFormat.RGBA32, false);

        Color[] Pixels = mapTexture.GetPixels();
        _texture.SetPixels(Pixels);
        _texture.Apply();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture(Shader.PropertyToID("_MainTex"), _texture);
        theSpriteRender.SetPropertyBlock(block);
    }

    public void SetMaterialInCircle(EMapMaterial Material, Vector2 WorldPosition, float WorldRadius) {
        int Radius = (int)ConvertValueInUnitsToValueInPixels(WorldRadius);
        int Diametr = Radius * 2;

        Vector2 PixelPosition = ConvertWorldPositionToPixelPosition(WorldPosition);
        int CenterX = (int)PixelPosition.x;
        int CenterY = (int)PixelPosition.y;


        //Debug.Log(CenterX + " : " + CenterY + " : " + Radius);
        SetMaterialInCircleByRect(Material, CenterX, CenterY, Radius);
    }

    private void SetMaterialInCircleByRect(EMapMaterial Material, int CenterX, int CenterY, int Radius) {
        int startPositionX = CenterX - Radius;
        int startPositionY = CenterY - Radius;
        //Debug.Log(startPositionY + " to " + (startPositionY + 2 * Radius) + " : " + startPositionX + " to " + (startPositionX + 2 * Radius) + " : "+ Radius);
        
        int Diametr = 2 * Radius;
        int aRadius = Radius;
        Radius = (int)(0.8 * aRadius);
        Debug.Log(Radius + " : " + aRadius);

        for (int y = 0; y < Diametr; ++y) {
            int DeltaY = aRadius - y;
            if ((startPositionY + y) < 0) continue;
            if ((startPositionY + y) > _texture.height) break;
                for (int x = 0; x < Diametr; ++x) {
                    if ((startPositionX + x) < 0) continue;
                    if ((startPositionX + x) > _texture.width) break;

                    int DeltaX = aRadius - x;
                    int DeltaRadius = (int)Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY);
                    if (DeltaRadius > aRadius) continue;

                    if (DeltaRadius <= aRadius && DeltaRadius >= Radius) {
                        //Debug.Log(DeltaRadius + " : " + aRadius + " : " + Radius);
                        Color color = _texture.GetPixel(startPositionX + x, startPositionY + y);
                        color.a = (0 != color.a) ? 0.5f : 0.0f;
                        _texture.SetPixel(startPositionX + x, startPositionY + y, color);
                    } else {
                    _texture.SetPixel(startPositionX + x, startPositionY + y, GetColorForMaterial(Material));
                    }
                    
                    
                }
        }

        _texture.Apply();
    }

    public Dictionary<EMapMaterial, int> GetMaterialsInCircle(Vector2 WorldPosition, float WorldRadius) {
        int Radius = (int)ConvertValueInUnitsToValueInPixels(WorldRadius);
        int Diametr = Radius * 2;

        Vector2 PixelPosition = ConvertWorldPositionToPixelPosition(WorldPosition);
        int CenterX = (int)PixelPosition.x;
        int CenterY = (int)PixelPosition.y;

        Dictionary<EMapMaterial, int> Result = new Dictionary<EMapMaterial, int>();

        Color[] BlockColors = _texture.GetPixels(CenterX - Radius, CenterY - Radius, Diametr, Diametr);

        for (int y = 0; y < Diametr; ++y) {
            int DeltaY = Radius - y;
            for (int x = 0; x < Diametr; ++x) {
                int DeltaX = Radius - x;

                int DeltaRadius = (int)Math.Sqrt(DeltaX * DeltaX + DeltaY * DeltaY);
                if (DeltaRadius > Radius) continue;

                EMapMaterial PixelMaterial = GetMaterialForColor(BlockColors[y * Diametr + x]);
                if (!Result.ContainsKey(PixelMaterial)) {
                    Result.Add(PixelMaterial, 0);
                }
                ++Result[PixelMaterial];
            }
        }

        return Result;
    }

    private float ConvertValueInUnitsToValueInPixels(float ValueInUnits) {
        float thePixelSizeInUnits = transform.localScale.x / _texture.width;
        return ValueInUnits / thePixelSizeInUnits;
    }

    private Vector2 ConvertWorldPositionToPixelPosition(Vector2 WorldPosition) {
        int theWidth = _texture.width;
        int theHeight = _texture.height;

        float theScaleX = transform.localScale.x;
        float theScaleY = transform.localScale.y;

        if (theScaleX/theScaleY != _texture.width/_texture.height) {
            Debug.Log("!!! INCORRECT MAP OBJECT ASPECT RATIO !!!");
        }

        //Here we have vector in local space
        // [-0.5 , -0.5] -> bottom left
        // [ 0.5 ,  0.5] -> top right
        Vector3 LocalPosition = transform.InverseTransformPoint(new Vector3(WorldPosition.x, WorldPosition.y, 0.0f));

        float thePixelPositionX = _texture.width * (0.5f + LocalPosition.x);
        float thePixelPositionY = _texture.height * (0.5f + LocalPosition.y);

        return new Vector2(thePixelPositionX, thePixelPositionY);
    }
}