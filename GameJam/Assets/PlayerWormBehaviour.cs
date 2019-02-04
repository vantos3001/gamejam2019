using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWormBehaviour : MonoBehaviour
{
    //------------------------------------- API ----------------------------------------
    //Types
    [System.Serializable]
    public struct BoneBlastSettings
    {
        public float radius;
        public float stunTime;
        public float pushAwayDistance;
        public float evationTime; //param that setup how we change angle on bone blasting
        public float boneDamagePenalty;
    }

    public delegate void OnBalstedDelegate();

    //Fields
    //-Settings
    public float _eatingRadius = 0.2f;
    public float _pointsPerFullMeatCircle = 10.0f;
    public float _penaltyIfNoEatAtTick = 0.01f;

    public BoneBlastSettings _boneBlastSettings;

    public float _initialPointsNumber = 100.0f;

    //-Events
    public OnBalstedDelegate onBlasted;

    //Methods
    public Values.LimitedFloat getPoints() { return _points; }

    //-------------------------------- Implementation ----------------------------------
    //Types
    private struct EatingTickInfo
    {
        public bool ateSomeMeatAtTick;
    }

    //Fields
    private PlayerMovement _playerMovement = null;

    //-Runtime
    private Values.LimitedFloat _points = new Values.LimitedFloat();

    private EatingTickInfo _eatingTickInfo;

    //Methods
    //-Initialization
    private void Start() {
        _playerMovement = gameObject.GetComponent<PlayerMovement>();

        _points.setValue(_initialPointsNumber);
    }

    //-Lifecycle
    //--Updating
    private void Update() {
        ResetEatingTickInfo();
        Update_PerformEat();

        Update_ChewingSoundWorkaround();

        Update_Hungry();
    }

    private void ResetEatingTickInfo() {
        _eatingTickInfo.ateSomeMeatAtTick = false;
    }

    private void Update_PerformEat() {
        Vector2 EatPosition = new Vector2(transform.position.x, transform.position.y);
        EatableWorld.EatableObjectWithPercent[] theEatableObjectsEatingDatas =
                getEatableWorld().GetObjectsPercentInCircle(EatPosition, _eatingRadius);

        foreach (EatableWorld.EatableObjectWithPercent theEatableObjectsEatingData in theEatableObjectsEatingDatas)
        {
            float AtePercent = theEatableObjectsEatingData.Percent;

            switch (theEatableObjectsEatingData.EatableObject.ObjectType)
            {
                case EatableObject.EEatableObjectType.Meat:   PerformEat_Meat(AtePercent);   break;
                case EatableObject.EEatableObjectType.Bone:   PerformEat_Bone();             break;
            }
        }

        getEatableWorld().EatInCircle(EatPosition, _eatingRadius);
    }

    private void Update_Hungry() {
        if (_playerMovement.waitingGameStart) return;

        if (_eatingTickInfo.ateSomeMeatAtTick) return;
        _points.changeValue(-_penaltyIfNoEatAtTick);
    }

    //--Eating implementation
    void PerformEat_Meat(float inPercent)
    {
        _points.changeValue(_pointsPerFullMeatCircle * inPercent);
        _eatingTickInfo.ateSomeMeatAtTick = true;
    }

    void PerformEat_Bone() {
        ProcessBoneBlast();
    }

    //---Bone blast
    private void ProcessBoneBlast()
    {
        Vector2 EatPosition = new Vector2(transform.position.x, transform.position.y);
        getEatableWorld().EatInCircle(EatPosition, _boneBlastSettings.radius);

        _points.changeValue(-_boneBlastSettings.boneDamagePenalty);

        _playerMovement.Rotate(180, _boneBlastSettings.evationTime);

        onBlasted();
    }

    //-Utils
    static EatableWorld getEatableWorld() { return Object.FindObjectOfType<EatableWorld>(); }

    //====================================================================
    //CODE TO REVIEW
    // Should be moved to some sounds manager
    private void Update_ChewingSoundWorkaround() {
        if (!FindObjectOfType<MediaController>().chewing.isPlaying) {
            FindObjectOfType<MediaController>().chewing.UnPause();
        } else {
            FindObjectOfType<MediaController>().chewing.Pause();
        }
    }
    //====================================================================
}
