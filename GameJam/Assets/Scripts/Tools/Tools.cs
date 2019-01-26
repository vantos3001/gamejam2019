using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static Vector2 ConvertVector3ToVector2(Vector3 vector3) {
        return new Vector2(vector3.x,vector3.y);
    }
}
