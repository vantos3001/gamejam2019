using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NextSceneButton : Button
{
    public override void OnPointerClick(PointerEventData eventData) {
        base.OnPointerClick(eventData);
        SwapSceneController.SwapScene();
    }
}
