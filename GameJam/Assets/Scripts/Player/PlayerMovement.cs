using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _rotationSensitivity = 5f;

    [SerializeField]
    private bool _isSmoothCamera;

    [Range(0.0f, 1.0f)]
    public float smoothTime = 0.5f;

    private bool _isFirstRotation;

    [Header("Shaking")]
    [SerializeField]
    private bool isShakeing = true;
    [SerializeField]
    private float angleSpeed = 0.05f;
    private float shakeAmplitude = 0;
    [SerializeField]
    private float maxShakeAmplitude = 20;
    private bool isGoRight = false;
    
    private float baseRotation = 0f;

    //private Transform[] _childTransforms;



    public GameObject BodyPrefab = null;
    public GameObject TailPrefab = null;
    public float FirstBodyElementScale = 1.0f;
    public float LastBodyElemntScale = 0.2f;

    public int BodyElementsCount = 0;

    private int _bodyElementsCountOld = 0;
    private GameObject[] _bodyElements = null;


    private void Start() { }

    void SetBodyElementsCount(int Count) { BodyElementsCount = Count; }

    private void UpdateBodyElements() {
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

    private void FixedUpdate() {
        MoveForward();
        RotatePlayer();

        UpdateBodyElements();
        UpdateBodyElementPositions();

        CameraFollow();
        UpdateShaking();

        SetRotation(baseRotation + shakeAmplitude);
    }

    private void MoveForward() {
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    private void RotatePlayer() {
        float mouseAngle = CalculateMouseAngle();
        
        float CurrentRotation = baseRotation;
        float DeltaAngle = Mathf.DeltaAngle(CurrentRotation, mouseAngle);

        if (Math.Abs(DeltaAngle) <= _rotationSensitivity) {
            CurrentRotation = mouseAngle;
            _isFirstRotation = false;
        } else {
            float DeltaSign = DeltaAngle / Math.Abs(DeltaAngle);
            float ActualRotationSpeed = _rotationSensitivity * DeltaSign;

            CurrentRotation += ActualRotationSpeed;

        }
        baseRotation = CurrentRotation;

        _isFirstRotation = true;
    }

    private float CalculateMouseAngle() {
        var mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dx = mouseCoords.x - transform.position.x;
        float dy = mouseCoords.y - transform.position.y;

        return (float)(Mathf.Rad2Deg * Math.Atan2(dy, dx));
    }

    private float GetRotation() { return transform.rotation.eulerAngles.z; }

    private void SetRotation(float Rotation) {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rotation);
    }

    private void CameraFollow() {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform;
        if (!_isSmoothCamera) {
            camera.position = new Vector3(transform.position.x, transform.position.y, camera.position.z);
        } else {
            Vector2 cameraVelocity = Vector2.zero;
            Vector2 smoothDamp = Vector2.SmoothDamp(new Vector2(camera.position.x, camera.position.y),
                new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), ref cameraVelocity,
                smoothTime);
            camera.position = new Vector3(smoothDamp.x, smoothDamp.y, camera.position.z);
        }
    }

    private void UpdateShaking() {
        if (!isShakeing) return;
        if (shakeAmplitude < -maxShakeAmplitude || shakeAmplitude > maxShakeAmplitude) {
            isGoRight = !isGoRight;
        }
        shakeAmplitude += (isGoRight) ? angleSpeed * Time.deltaTime : -angleSpeed * Time.deltaTime;
    }
}
