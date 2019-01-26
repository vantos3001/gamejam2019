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

    private Transform[] _childTransforms;

    private void Start() {
        var gameObjects = GameObject.FindGameObjectsWithTag("PlayerBody");
        _childTransforms = new Transform[gameObjects.Length];
        for (int i = 0, count = gameObjects.Length; i < count; i++) {
            _childTransforms[i] = gameObjects[i].transform;
        }
    }

    private void Update() {
    }

    private void FixedUpdate() {
        MoveForward();
        RotatePlayer();
        CameraFollow();
    }

    private void MoveForward() {
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    private void RotatePlayer() {
        float mouseAngle = CalculateMouseAngle();

        float oldRoration = GetRotation();
        float CurrentRotation = GetRotation();
        float DeltaAngle = Mathf.DeltaAngle(CurrentRotation, mouseAngle);
               
        if (Math.Abs(DeltaAngle) <= _rotationSensitivity) {
            CurrentRotation = mouseAngle;
            _isFirstRotation = false;
        } else {
            float DeltaSign = DeltaAngle / Math.Abs(DeltaAngle);
            float ActualRotationSpeed = _rotationSensitivity * DeltaSign;
        
            CurrentRotation += ActualRotationSpeed;

        }

        SetRotation(CurrentRotation);
        
        RotateChildren(oldRoration);
        
        _isFirstRotation = true;
    }

    private void RotateChildren(float oldAngle) {
        foreach (var childTransform in _childTransforms) {
            var childAngle = transform.rotation.eulerAngles.z;
            childTransform.rotation = Quaternion.Euler(childTransform.rotation.x, childTransform.rotation.y, oldAngle);
            oldAngle = childAngle;
        }
    }

    private float CalculateMouseAngle() {
        var mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dx = mouseCoords.x - transform.position.x;
        float dy = mouseCoords.y - transform.position.y;
        
        return (float) (Mathf.Rad2Deg * Math.Atan2(dy, dx));
    }

    private float GetRotation() {
        return transform.rotation.eulerAngles.z;
    }
    
    private void SetRotation(float Rotation) {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rotation);
    }

    private void CameraFollow() {
        Transform camera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform;
        if (!_isSmoothCamera) {
            camera.position = new Vector3(transform.position.x, transform.position.y,  camera.position.z);
        } else {
            Vector2 cameraVelocity = Vector2.zero;
            Vector2 smoothDamp = Vector2.SmoothDamp(new Vector2(camera.position.x, camera.position.y),
                new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), ref cameraVelocity,
                smoothTime);
            camera.position = new Vector3(smoothDamp.x,smoothDamp.y, camera.position.z);  
        }
    }
}
