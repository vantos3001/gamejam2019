using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableWorld : MonoBehaviour
{
    public struct EatableObjectWithPercent
    {
        public EatableObject EatableObject;
        public float Percent;
    }
        
    public void EatInCircle(Vector2 WorldPosition, float WorldRadius){
        EatableObject[] EatableObjects = Object.FindObjectsOfType<EatableObject>();
        
        foreach (EatableObject EatableObject in EatableObjects){
            EatableObject.EatInCircle(WorldPosition, WorldRadius);
        }
    }

    public EatableObjectWithPercent[] GetObjectsPercentInCircle(Vector2 WorldPosition, float WorldRadius){
        EatableObject[] EatableObjects = Object.FindObjectsOfType<EatableObject>();
        EatableObjectWithPercent[] Result = new EatableObjectWithPercent[EatableObjects.Length];

        for (int theIndex = 0; theIndex < EatableObjects.Length; ++theIndex){
            EatableObject theEatableObject = EatableObjects[theIndex];
            Result[theIndex].EatableObject = theEatableObject;
            Result[theIndex].Percent = theEatableObject.GetObjectPercentInCircle(WorldPosition, WorldRadius);
        }

        return Result;
    }
}
