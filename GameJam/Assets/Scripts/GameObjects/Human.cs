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
    //-Human parts setting
    public PartSettings MeatEatablePart;
    public PartSettings HeartEatablePart;
    public PartSettings Kidney1EatablePart;
    public PartSettings Kidney2EatablePart;
    public PartSettings LiverEatablePart;
    public PartSettings LungsEatablePart;
    public PartSettings StomachEatablePart;

    PartSettings[] Parts;

    //Methods
    private void Start()
    {
        //We use static array for performance. For more flexibility use List<PartSettings>
        const int PartsLimitCount = 10;
        Parts = new PartSettings[PartsLimitCount];

        Parts[0] = MeatEatablePart;
        Parts[1] = HeartEatablePart;
        Parts[2] = Kidney1EatablePart;
        Parts[3] = Kidney2EatablePart;
        Parts[0] = LiverEatablePart;
        Parts[5] = LungsEatablePart;
        Parts[6] = StomachEatablePart;
}

    //-State accessors
    public bool IsDead(){ return (Damage.Death == GetTotalDamage()); }

    public Damage GetTotalDamage() {
        foreach(PartSettings Part in Parts) {
            if (Part.IsImportant && Part.PartEatableObject.GetAtePercent() > Part.CriticalAtePercent) return Damage.Death;
        }
 
        return Damage.Medium;
    }
    
    public float GetTotalPoints() { return MeatEatablePart.PartEatableObject.GetLeftToEatPercent(); }

    public PartSettings GetMeatState() { return MeatEatablePart; }
    public PartSettings GetHeartState() { return HeartEatablePart; }
    public PartSettings GetKidney1State() { return Kidney1EatablePart; }
    public PartSettings GetKidney2State() { return Kidney2EatablePart; }
    public PartSettings GetLiverState() { return LiverEatablePart; }
    public PartSettings GetLungsState() { return LungsEatablePart; }
    public PartSettings GetStomachState() { return StomachEatablePart; }
}
