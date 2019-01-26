using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
      _hud = HUDGameObject.GetComponent<HUD>();
      _gameController = FindObjectOfType<GameController>();
   }

   private void Update() {
      _hud.SetScoreText(_gameController.GetCurrentScore());
      _hud.SetHumanHealthPerCent(_gameController.GetHumanPerCent());
   }
   
   //--Show & Hide
   public void ShowHUD() { HUDGameObject.gameObject.SetActive(true); }
   public void HideHUD() { HUDGameObject.gameObject.SetActive(false); }
   
   public void ShowWinPanel() { WinPanel.SetActive(true); }
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
