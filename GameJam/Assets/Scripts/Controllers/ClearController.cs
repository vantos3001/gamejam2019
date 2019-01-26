using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearController : MonoBehaviour {
    
    //Fields
    //-Settings
    public float EatingRadius = 0.2f;
    public float PointsPerMeatPixel = 0.001f;
    public float PenaltyIfNoEatAtTick = 0.1f;
    
    //--Runtime-cache
    private PlayerMovement _playerMovement = null;
    
    //-Runtime
    public float CurrentPoits = 0.0f;
    
    private bool _eatTickInfo_CanEat = true;
    private bool _eatTickInfo_AteSomeMeat = false;
    
    // Update is called once per frame
    private void Start(){
        _playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    void Update(){
        Update_ResetEatTickStatistic();        
        Update_PerformEatPrediction();
        Update_ProcessEatTickStatistic();
    }

    private void Update_ResetEatTickStatistic() {
        _eatTickInfo_CanEat = true;
        _eatTickInfo_AteSomeMeat = false;
    }

    private void Update_PerformEatPrediction() {
        //EatableWorld theEatableWorld = Object.FindObjectOfType<EatableWorld>();
        //theEatableWorld.GetObjectsPercentInCircle(EatPosition, EatingRadius);
    }
    
    private void Update_EatCircle() {
        EatableWorld theEatableWorld = Object.FindObjectOfType<EatableWorld>();

        Vector2 EatPosition = new Vector2(transform.position.x, transform.position.y);
        theEatableWorld.EatInCircle(EatPosition, EatingRadius);
    }

    private void Update_ProcessEatTickStatistic() {
        //if (!EatTickStatistic_AteSomeMeat){
        //    DecreasePointsInNoMeat();
        //}
    }

    private void DecreasePointsInNoMeat(){
        //CurrentPoits -= PenaltyIfNoEatAtTick;
    }
}
