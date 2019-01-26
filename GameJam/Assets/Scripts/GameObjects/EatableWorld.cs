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

        List<EatableObjectWithPercent> ResultDynamic = new List<EatableObjectWithPercent>();
        for (int theIndex = 0; theIndex < EatableObjects.Length; ++theIndex)
        {
            EatableObject theEatableObject = EatableObjects[theIndex];
            float Percent = theEatableObject.GetObjectPercentInCircle(WorldPosition, WorldRadius);
            if (Percent == 0.0f) continue;

            EatableObjectWithPercent theEatableObjectWithPercent = new EatableObjectWithPercent();
            theEatableObjectWithPercent.EatableObject = theEatableObject;
            theEatableObjectWithPercent.Percent = Percent;
            
            ResultDynamic.Add(theEatableObjectWithPercent);
        }
        
        EatableObjectWithPercent[] Result = new EatableObjectWithPercent[ResultDynamic.Count];
        int theResultIndex = 0;
        foreach (EatableObjectWithPercent theEatableObjectWithPercent in ResultDynamic){
            Result[theResultIndex++] = theEatableObjectWithPercent;
        }
        
        return Result;
    }
}
