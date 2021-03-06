﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Fields
    //-Settings
    [Header("Settings")]
    public float Speed = 3f;
    public float RotationSensitivity = 5f;

    public bool IsSmoothCamera = false;
    public float SmoothTime = 0.5f;

    //--Shaking
    [Header("Shaking")]
    [SerializeField]
    public bool IsShakeing = true;
    
    public float AngleSpeed = 0.05f;
    private float shakeAmplitude = 0;
    public float maxShakeAmplitude = 20;

    //--Body
    [Header("Body")]
    public GameObject BodyPrefab = null;
    public GameObject TailPrefab = null;
    public float FirstBodyElementScale = 1.0f;
    public float LastBodyElemntScale = 0.2f;
    public int BodyElementsCount = 0;
    public bool waitingGameStart = true;
    public float angleWaitingSpeed = 10f;

    //-Runtime
    private float baseRotation = 0f;
    
    //--Shaking
    private bool isGoRight = false;

    //--Body
    private int _bodyElementsCountOld = 0;
    private GameObject[] _bodyElements = null;    

    //--Stunning
    private float _stunTime = 0.0f;
    private bool needToRotate = false;
    private float evateTime;
    
    //Methods
    private void FixedUpdate() {

        if (_stunTime > 0.0f) {
            _stunTime -= Time.deltaTime;
        } else{
            _stunTime = 0.0f;
            
            MoveForward();
            if(!waitingGameStart)RotatePlayer();

            UpdateBodyElements();
            UpdateBodyElementPositions();
            UpdateShaking();
            
            SetRotation(baseRotation + shakeAmplitude);
        }
        if (waitingGameStart) Rotate(angleWaitingSpeed, 0.3f);
        UpdateEvateTime();
        CameraFollow();
    }
    private void Update() {
        if (Input.GetMouseButtonDown(0)){
            waitingGameStart = false;
        }
    }
    //-Body update
    private void UpdateBodyElements() {
        
        //Initialize body
        if (_bodyElementsCountOld == BodyElementsCount) return;
        if (0 == BodyElementsCount) {
            _bodyElements = null;
            return;
        }

        //Update body objects reusing old size
        GameObject[] theBodyElementsOld = _bodyElements;
        _bodyElements = new GameObject[BodyElementsCount];

        int theSavedElementsCount = Math.Min(
            BodyElementsCount, (null != theBodyElementsOld) ? theBodyElementsOld.Length : 0
        );
        for (int i = 0; i < theSavedElementsCount; ++i) {
            _bodyElements[i] = theBodyElementsOld[i];

            Vector3 theNewScale = _bodyElements[i].transform.localScale;
            theNewScale.y = LastBodyElemntScale +
                            (FirstBodyElementScale - LastBodyElemntScale) * ((float)i / (BodyElementsCount - 1));
            _bodyElements[i].transform.localScale = theNewScale;
        }

        Vector3 theNextElementPosition = (0 != theSavedElementsCount) ?
                _bodyElements[theSavedElementsCount - 1].transform.position : gameObject.transform.position;

        for (int i = theSavedElementsCount; i < BodyElementsCount - 1; ++i) {
            _bodyElements[i] = Instantiate(BodyPrefab, theNextElementPosition, transform.rotation);
            _bodyElements[i].GetComponent<WormBodyElement>().SetNextPosition(theNextElementPosition);
            theNextElementPosition = _bodyElements[i].transform.position;
            
            Vector3 theNewScale = _bodyElements[i].transform.localScale;
            theNewScale.y = FirstBodyElementScale -
                            (FirstBodyElementScale - LastBodyElemntScale) * ((float)i / (BodyElementsCount - 1));
            _bodyElements[i].transform.localScale = theNewScale;
        }

        _bodyElements[BodyElementsCount - 1] = Instantiate(TailPrefab, theNextElementPosition, transform.rotation);
        _bodyElements[BodyElementsCount - 1].GetComponent<WormBodyElement>().SetNextPosition(theNextElementPosition);
        theNextElementPosition = _bodyElements[BodyElementsCount - 1].transform.position;

        theBodyElementsOld = null;
        _bodyElementsCountOld = BodyElementsCount;
    }
    
    private void UpdateBodyElementPositions() {
        if (0 == BodyElementsCount) return;

        Vector3 theNextElementPosition = gameObject.transform.position;
        for (int i = 0; i < BodyElementsCount; ++i) {
            WormBodyElement theWormBodyElement = _bodyElements[i].GetComponent<WormBodyElement>();
            theWormBodyElement.ApplyNextPosition();
            theWormBodyElement.SetNextPosition(theNextElementPosition);
            theNextElementPosition = _bodyElements[i].transform.position;
        }
    }
    
    //-Moving by mouse
    private void MoveForward() {
        transform.position += transform.right * Speed * Time.deltaTime;
    }

    private void RotatePlayer() {
        float mouseAngle = CalculateMouseAngle();
        
        float CurrentRotation = baseRotation;
        float DeltaAngle = Mathf.DeltaAngle(CurrentRotation, mouseAngle);

        if (Math.Abs(DeltaAngle) <= RotationSensitivity) {
            CurrentRotation = mouseAngle;
        } else {
            float DeltaSign = DeltaAngle / Math.Abs(DeltaAngle);
            float ActualRotationSpeed = RotationSensitivity * DeltaSign;

            CurrentRotation += ActualRotationSpeed;

        }
        baseRotation = CurrentRotation;
    }

    private float CalculateMouseAngle() {
        var mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dx = mouseCoords.x - transform.position.x;
        float dy = mouseCoords.y - transform.position.y;
        if (needToRotate) {
            dx = -dx;
            dy = -dy;
        }
        if (waitingGameStart) {
            dx = 0;
            dy = 0;
        }
        return (float)(Mathf.Rad2Deg * Math.Atan2(dy, dx));
    }

    private float GetRotation() { return transform.rotation.eulerAngles.z; }

    private void SetRotation(float Rotation) {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rotation);
    }

    //-Camera following
    private void CameraFollow() {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform;
        if (!IsSmoothCamera) {
            camera.position = new Vector3(transform.position.x, transform.position.y, camera.position.z);
        } else {
            Vector2 cameraVelocity = Vector2.zero;
            Vector2 smoothDamp = Vector2.SmoothDamp(new Vector2(camera.position.x, camera.position.y),
                new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), ref cameraVelocity,
                SmoothTime);
            camera.position = new Vector3(smoothDamp.x, smoothDamp.y, camera.position.z);
        }
    }

    //-Shaking
    private void UpdateShaking() {
        if (!IsShakeing) return;
        if (shakeAmplitude < -maxShakeAmplitude || shakeAmplitude > maxShakeAmplitude) {
            isGoRight = !isGoRight;
        }
        shakeAmplitude += (isGoRight) ? AngleSpeed * Time.deltaTime : -AngleSpeed * Time.deltaTime;
    }
    
    //-Stuning
    public void SetStunTime(float InStunTime){ _stunTime = InStunTime; }
    
    //-Pushing away
    public void PushAway(float Distance){
        transform.position -= transform.right * Distance;
        
        foreach (GameObject theBodyElement in _bodyElements){
            theBodyElement.transform.position = transform.position;
            theBodyElement.GetComponent<WormBodyElement>().SetNextPosition(transform.position);
        }
    }

    public void Rotate(float angle, float time) {
        needToRotate = true;
        evateTime = time;
        baseRotation += angle;
    }

    private void UpdateEvateTime() {
        if(evateTime > 0) {
            evateTime -= Time.deltaTime;
        } else {

            needToRotate = false;
        }
    }
}
