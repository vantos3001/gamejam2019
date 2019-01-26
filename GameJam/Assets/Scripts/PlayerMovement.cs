using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float _speed = 3f;

    [SerializeField]
    private float _rotationSensitivity = 5f;
    
    void Start() {
    }

    private void Update() {
        var mouseCoords = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float dx = mouseCoords.x - transform.position.x;
        float dy = mouseCoords.y - transform.position.y;

        float mouseAngle = (float) (Mathf.Rad2Deg * Math.Atan2(dy, dx));

        float CurrentRotation = GetRotation();
        float DeltaAngle = Mathf.DeltaAngle(CurrentRotation, mouseAngle);
               
        if (Math.Abs(DeltaAngle) <= _rotationSensitivity) {
            CurrentRotation = mouseAngle;
        } else {
            float DeltaSign = DeltaAngle / Math.Abs(DeltaAngle);
            float ActualRotationSpeed = _rotationSensitivity * DeltaSign;
        
            CurrentRotation += ActualRotationSpeed;
        }

        SetRotation(CurrentRotation);
    }

    private void FixedUpdate() {
        MoveForward();
    }

    private void MoveForward() {
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    private float GetRotation() {
        return transform.rotation.eulerAngles.z;
    }
    
    private void SetRotation(float Rotation) {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rotation);
    }
}
