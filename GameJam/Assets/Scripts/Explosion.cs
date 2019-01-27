using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float explodeTime = 1f;
    private float time;
    public Image image;
    private bool isExplode = false;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
            explode();
        }
        if (isExplode) {
            if (time < 0) isExplode = false;
            
            float lerp = Mathf.Lerp(0f, 1f, time / explodeTime);
            changeAlfaChannel(lerp);
            time -= Time.deltaTime;
        }
    }

    public void explode() {
        
        changeAlfaChannel(1f);
        time = explodeTime;
        isExplode = true;
        Debug.Log("exo!!");
    }

    private void changeAlfaChannel(float alfa) {
        Color color = image.color;
        Debug.Log(color.a);
        
        color.a = alfa;
        image.color = color;
    }
}
