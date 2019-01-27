using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [System.Serializable]
    public struct PartSettings {
        public EatableObject PartEatableObject;
        public float CriticalAtePercent;
        public bool IsImportant;
    }

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
    public PartSettings MeatEatablePart;

    public PartSettings HeartEatablePart;
    public PartSettings Kidney1EatablePart;
    public PartSettings Kidney2EatablePart;
    public PartSettings LiverEatablePart;
    public PartSettings LungsEatablePart;
    public PartSettings StomachEatablePart;

    //Methods
    //-State accessors
    public bool IsDead(){ return (Damage.Death == GetTotalDamage()); }

    public Damage GetTotalDamage() { return GetMeatDamage(); }
    
    public Damage GetMeatDamage() {
        float LeftToEatPercent = MeatEatablePart.PartEatableObject.GetLeftToEatPercent();
        if (LeftToEatPercent > 0.9f) return Damage.None;
        if (LeftToEatPercent > 0.7f) return Damage.Low;
        if (LeftToEatPercent > 0.5f) return Damage.Medium;
        if (LeftToEatPercent > 0.4f) return Damage.Hight;
        if (LeftToEatPercent > MaxMeatLeftToEatPercent) return Damage.Critical;
        return Damage.Death;
    }

    public float GetTotalPoints() {
        float LeftToEatPercent = MeatEatablePart.PartEatableObject.GetLeftToEatPercent();
        return LeftToEatPercent;
    }

    public PartSettings GetHeartState() { return HeartEatablePart; }
    public PartSettings GetKidney1State() { return Kidney1EatablePart; }
    public PartSettings GetKidney2State() { return Kidney2EatablePart; }
    public PartSettings GetLiverState() { return LiverEatablePart; }
    public PartSettings GetLungsState() { return LungsEatablePart; }
    public PartSettings GetStomachState() { return StomachEatablePart; }
}
