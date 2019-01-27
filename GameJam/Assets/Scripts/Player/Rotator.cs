using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField]
    private bool isShakeing = true;
    [SerializeField]
    private float maxShakeAmplitude = 9f;
    private float shakeAmplitude = 0f;
    private bool isGoRight = false;
    [SerializeField]
    private float angleSpeed = 1.1f;
    private float baserotation;

    // Start is called before the first frame update
    void Start()
    {
        baserotation = GetRotation();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        baserotation = GetRotation();
        UpdateShaking();
        SetRotation(baserotation - shakeAmplitude);
    }

    private float GetRotation() { return transform.rotation.eulerAngles.z; }

    private void SetRotation(float Rotation) {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Rotation);
    }

    private void UpdateShaking() {
        if (!isShakeing) return;
        if (shakeAmplitude < -maxShakeAmplitude || shakeAmplitude > maxShakeAmplitude) {
            isGoRight = !isGoRight;
        }
        //Debug.Log(shakeAmplitude);
        shakeAmplitude += (isGoRight) ? angleSpeed * Time.deltaTime : -angleSpeed * Time.deltaTime;
    }
}
