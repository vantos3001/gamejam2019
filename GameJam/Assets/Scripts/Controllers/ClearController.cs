using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearController : MonoBehaviour {
    
    //Fields
    //-Settings
    public float EatingRadius = 0.2f;
    public float PointsPerFullMeatCircle = 10.0f;
    public float PenaltyIfNoEatAtTick = 0.01f;
    
    //--Runtime-cache
    private PlayerMovement _playerMovement = null;
    
    //-Runtime
    public float CurrentPoits = 0.0f;
    
    private bool _eatTickInfo_CanEat = true;
    private float _eatTickInfo_AteMeatCirclePercent = 0.0f;
    
    //Methods
    //-API
    public float GetCurrentPoints(){
        return CurrentPoits;
    }

    //-Implementation
    private void Start(){
        _playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    void Update() {
        Update_ResetEatTickStatistic();        
        Update_PerformEat();
        Update_ProcessEatTickStatistic();
    }

    //Eating info lifecycle
    private void Update_ResetEatTickStatistic() {
        _eatTickInfo_CanEat = true;
        _eatTickInfo_AteMeatCirclePercent = 0.0f;
    }

    private void Update_PerformEat(){
        Vector2 EatPosition = new Vector2(transform.position.x, transform.position.y);

        EatableWorld theEatableWorld = Object.FindObjectOfType<EatableWorld>();
        EatableWorld.EatableObjectWithPercent[] theEatableObjectsEatingDatas =
                theEatableWorld.GetObjectsPercentInCircle(EatPosition, EatingRadius);

        foreach (EatableWorld.EatableObjectWithPercent theEatableObjectsEatingData in theEatableObjectsEatingDatas) {
            switch (theEatableObjectsEatingData.EatableObject.ObjectType){
                case EatableObject.EEatableObjectType.Meat:
                    _eatTickInfo_AteMeatCirclePercent += theEatableObjectsEatingData.Percent;
                    break;
            }
        }

        theEatableWorld.EatInCircle(EatPosition, EatingRadius);
    }
    
    private void Update_ProcessEatTickStatistic() {
        if (_eatTickInfo_AteMeatCirclePercent != 0.0f){
            CurrentPoits += PointsPerFullMeatCircle * _eatTickInfo_AteMeatCirclePercent;
        } else {
            CurrentPoits -= PenaltyIfNoEatAtTick;
        }
    }
}
