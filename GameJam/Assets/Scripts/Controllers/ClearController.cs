using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearController : MonoBehaviour {
    
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private TextureSetupScript _meat;
    
    // Update is called once per frame
    void Update() {
        ClearCircle(transform.position.x, transform.position.y, 0.2f);
    }

    private void ClearCircle(float x, float y, float Radius) {
        _meat.SetMaterialInCircle(TextureSetupScript.EMapMaterial.Empty, new Vector2(x,y), Radius);
    }
}
