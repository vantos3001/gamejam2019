using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float explodeTime = 1f;
    private float time = 0.0f;
    public float ExplistionStartAlpha = 0.8f;

    public Image image;
    private bool isExplode = false;
    void Start()
    {
        //TODO: Find better architecture
        FindObjectOfType<PlayerWormBehaviour>().onBlasted += explode;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isExplode) return;
        if (time < 0.0f) isExplode = false;
            
        float theAlpha = Mathf.Lerp(0.0f, ExplistionStartAlpha, time / explodeTime);
        changeAlfaChannel(theAlpha);
        time -= Time.deltaTime;
    }

    public void explode() {
        time = explodeTime;
        isExplode = true;
    }

    private void changeAlfaChannel(float alfa) {
        Color color = image.color;
        color.a = alfa;
        image.color = color;
    }
}
