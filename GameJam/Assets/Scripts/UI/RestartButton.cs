using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestartButton : Button {

    public override void OnPointerClick(PointerEventData eventData) {
        base.OnPointerClick(eventData);
        SwapSceneController.RestartGame();
    }
    
} 
