using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UIManager : MonoBehaviour {
   public Canvas BaseCanvas;

   public GameObject HUDGameObject;
   public GameObject WinPanel;
   public GameObject LosePanel;
   public GameObject NextScenePanel;
   public GameObject ReadyToWinPanel;

   private static UIManager _instance;

   private GameController _gameController;
   private HUD _hud;
   
   private void Start() {
      _gameController = FindObjectOfType<GameController>();
      
      _hud = HUDGameObject.GetComponent<HUD>();
      _hud.MaxScore = _gameController.WinScore;
   }

   private void Update() {
      _hud.SetCurrentScore(_gameController.GetCurrentScore());
      _hud.SetHumanHealth(_gameController.GetHumanHealth());

      //organs
      Human theHuman = _gameController.Human;

      _hud.SetHeartColor(theHuman.GetHeartState());
      _hud.SetLiverColor(theHuman.GetLiverState());
      _hud.SetLungsColor(theHuman.GetLungsState());
      _hud.SetStomachColor(theHuman.GetStomachState());
      _hud.SetKidneysColor(theHuman.GetKidney1State(), theHuman.GetKidney2State());
      
      _hud.SetHumanHealthPerCent(_gameController.GetHumanPerCent());
   }
   
   //--Show & Hide
   public void ShowHUD() { HUDGameObject.gameObject.SetActive(true); }
   public void HideHUD() { HUDGameObject.gameObject.SetActive(false); }
   
   public void ShowWinPanel() {
        SwapSceneController.WatchVideo();

        HideHUD();
   }
   public void HideWinPanel() { WinPanel.SetActive(false); }
   
   public void ShowLosePanel() { LosePanel.SetActive(true); }
   public void HideLosePanel() { LosePanel.SetActive(false); }

   public void ShowNextScenePanel() { NextScenePanel.SetActive(true); }
   public void HideNextScenePanel() { NextScenePanel.SetActive(false); }
   
   public void ShowReadyToWinPanel(){ReadyToWinPanel.SetActive(true);}
   public void HideReadyToWinPanel(){ReadyToWinPanel.SetActive(false);}

   public void HideAllPanels() {
      HideHUD();
      HideWinPanel();
      HideLosePanel();
      HideNextScenePanel();
      HideReadyToWinPanel();
   }
}
