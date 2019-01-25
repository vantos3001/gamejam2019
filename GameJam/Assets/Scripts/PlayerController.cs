using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float _speed = 1f;
    
    private Vector2 _mousePosition;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {
        _mousePosition = Input.mousePosition;
        _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
        _mousePosition -= (Vector2) transform.position;

        var quotient = Mathf.Sqrt(_mousePosition.x * _mousePosition.x + _mousePosition.y * _mousePosition.y) / _speed;
        _mousePosition /= quotient;
        
        transform.Translate(_mousePosition * Time.deltaTime * _speed);
    }
}
