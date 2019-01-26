using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBodyElement : MonoBehaviour {
    private Vector3 _nextPosition;

    public void SetNextPosition(Vector3 NewNextPosition) { _nextPosition = NewNextPosition; }

    public void ApplyNextPosition() {
        Vector3 Delta = _nextPosition - transform.position;
        
        transform.position = _nextPosition;
        
        if (0 == Delta.magnitude) return;        
        SetRotation(Mathf.Rad2Deg * Mathf.Atan2(Delta.y, Delta.x));
    }
    
    private void SetRotation(float Rotation) {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rotation);
    }
}
