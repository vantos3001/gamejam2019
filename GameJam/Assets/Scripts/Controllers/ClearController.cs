using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearController : MonoBehaviour {
    
    //Fields
    //-Settings
    //--Eating
    public float EatingRadius = 0.2f;
    public float PointsPerFullMeatCircle = 10.0f;
    public float PenaltyIfNoEatAtTick = 0.01f;

    //--Bone blast settings
    public float BoneBlastRadius = 0.4f;
    public float BoneBlashStunTime = 2.0f;
    public float BoneBlashPushAwayDistance = 2.0f;
    public float evationTime = 0.5f; //param that setup how we change angle on bone blasting
    
    //--Runtime-cache
    private PlayerMovement _playerMovement = null;
    
    //-Runtime
    public float CurrentPoits = 0.0f;
    public float BoneDamagePenalty = 200.0f;

    private bool _eatTickInfo_AteSomeMeatAtTick = false;

    //Methods
    //-API
    public float GetCurrentPoints() { return CurrentPoits; }

    //TODO: Split methods to API and implementation
    private void SetCurrentPoints(float NewCurrentPoints) { CurrentPoits = Mathf.Clamp(NewCurrentPoints, 0.0f, float.MaxValue); }
    private void ChangeCurrentPoints(float PointsDelta) { SetCurrentPoints(GetCurrentPoints() + PointsDelta); }

    //-Implementation
    //--Initialization
    private void Start() {
        _playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    //--Lifecycle
    void Update() {
        Update_ChewingSoundWorkaround();
        Update_PerformEat();

        if (_playerMovement.waitingGameStart) return;
        Update_Hungry();
    }

    private void Update_PerformEat() {
        Vector2 EatPosition = new Vector2(transform.position.x, transform.position.y);

        EatableWorld theEatableWorld = Object.FindObjectOfType<EatableWorld>();
        EatableWorld.EatableObjectWithPercent[] theEatableObjectsEatingDatas =
                theEatableWorld.GetObjectsPercentInCircle(EatPosition, EatingRadius);

        foreach (EatableWorld.EatableObjectWithPercent theEatableObjectsEatingData in theEatableObjectsEatingDatas) {
            float AtePercent = theEatableObjectsEatingData.Percent;

            switch (theEatableObjectsEatingData.EatableObject.ObjectType) {
                case EatableObject.EEatableObjectType.Meat:   PerformEat_Meat(AtePercent);   break;
                case EatableObject.EEatableObjectType.Bone:   PerformEat_Bone();             break;
            }
        }

        theEatableWorld.EatInCircle(EatPosition, EatingRadius);
    }

    private void Update_Hungry() {
        if (!_eatTickInfo_AteSomeMeatAtTick) {
            ChangeCurrentPoints(-PenaltyIfNoEatAtTick);
        }

        _eatTickInfo_AteSomeMeatAtTick = false;
    }

    //---Eating implementation
    void PerformEat_Meat(float Percent) {
        ChangeCurrentPoints(PointsPerFullMeatCircle * Percent);
        _eatTickInfo_AteSomeMeatAtTick = true;
    }

    void PerformEat_Bone() {
        ProcessBoneBlast();
    }

    //---Bone blast
    private void ProcessBoneBlast() {
        //Make ate circle in eatable objects
        Vector2 EatPosition = new Vector2(transform.position.x, transform.position.y);
        EatableWorld theEatableWorld = Object.FindObjectOfType<EatableWorld>();
        theEatableWorld.EatInCircle(EatPosition, BoneBlastRadius);

        //Apply points penalty
        ChangeCurrentPoints(-BoneDamagePenalty);

        //Move back
        _playerMovement.Rotate(180, evationTime);

        //Notify about blast
        //====================================================================
        //CODE TO REVIEW
        //Should be moved to UI logic
        FindObjectOfType<Explosion>().explode();
        //====================================================================
    }

    //====================================================================
    //CODE TO REVIEW
    // Should be moved to some sounds manager
    private void Update_ChewingSoundWorkaround()
    {
        if (!FindObjectOfType<MediaController>().chewing.isPlaying)
        {
            FindObjectOfType<MediaController>().chewing.UnPause();
        }
        else
        {
            FindObjectOfType<MediaController>().chewing.Pause();
        }
    }
    //====================================================================
}
