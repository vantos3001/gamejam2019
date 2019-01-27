using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public enum Damage
    {
        None,
        Low,
        Medium,
        Hight,
        Critical,
        Death
    }
    
    //Fields
    //-Settings
    public float MaxMeatLeftToEatPercent = 0.2f;
    
    //-Human parts setting
    public EatableObject MeatEatableObject = null;

    public EatableObject HeartEatableObject = null;
    public EatableObject Kidney1EatableObject = null;
    public EatableObject Kidney2EatableObject = null;
    public EatableObject LiverEatableObject = null;
    public EatableObject LungsEatableObject = null;
    public EatableObject StomachEatableObject = null;
    
    //Methods
    //-State accessors
    public bool IsDead(){ return (Damage.Death == GetTotalDamage()); }

    public Damage GetTotalDamage() { return GetMeatDamage(); }
    
    public Damage GetMeatDamage() {
        float LeftToEatPercent = MeatEatableObject.GetLeftToEatPercent();
        if (LeftToEatPercent > 0.9f) return Damage.None;
        if (LeftToEatPercent > 0.7f) return Damage.Low;
        if (LeftToEatPercent > 0.5f) return Damage.Medium;
        if (LeftToEatPercent > 0.4f) return Damage.Hight;
        if (LeftToEatPercent > MaxMeatLeftToEatPercent) return Damage.Critical;
        return Damage.Death;
    }

    public float GetTotalPoints() {
        float LeftToEatPercent = MeatEatableObject.GetLeftToEatPercent();
        return LeftToEatPercent;
    }

    EatableObject GetHeartEatableObject() { return HeartEatableObject; }
    EatableObject GetKidney1EatableObject() { return Kidney1EatableObject; }
    EatableObject GetKidney2EatableObject() { return Kidney2EatableObject; }
    EatableObject GetLiverEatableObject() { return LiverEatableObject; }
    EatableObject GetLungsEatableObject() { return LungsEatableObject; }
    EatableObject GetStomachEatableObject() { return StomachEatableObject; }
}
