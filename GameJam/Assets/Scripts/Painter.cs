using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particle;
    private bool lkmPressed = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
            lkmPressed = true;
        }
        if (Input.GetMouseButtonUp(0)) {
            lkmPressed = false;
        }

        if (lkmPressed){
            //Debug.Log(Input.mousePosition);

            Vector3 pos = Input.mousePosition;
            pos.z = 20;
            pos = Camera.main.ScreenToWorldPoint(pos);
            //Debug.Log(pos.x + ':' + pos.y);
            //Instantiate(particle, pos, transform.rotation);
            ClearCircle(pos.x, pos.y, 1);
        }
    }

    private void ClearCircle(float x, float y, float Radius) {
        GameObject.Find("Square").GetComponent<TextureSetupScript>().SetMaterialInCircle(TextureSetupScript.EMapMaterial.Empty, new Vector2(x, y), Radius);
    }
}
