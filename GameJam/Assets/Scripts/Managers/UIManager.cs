using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
   public Canvas BaseCanvas;

   public GameObject HUDGameObject;
   public GameObject WinPanel;
   public GameObject LosePanel;
   public GameObject NextScenePanel;

   private static UIManager _instance;

   private ClearController _playerClearController = null;
   private Human _human = null;
   
   private void Start() {
      _hud = HUDGameObject.GetComponent<HUD>();

      _playerClearController = Object.FindObjectOfType<ClearController>();
      _human = Object.FindObjectOfType<Human>();
   }

   private void Update() {
      //TODO: Just set here UI state from objects state. It's better then base UI updates on event
      
      //_hud.SetScoreText((int)_playerClearController.GetCurrentPoints());
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

   public void HideAllPanels() {
      HideHUD();
      HideWinPanel();
      HideLosePanel();
      HideNextScenePanel();
   }

   private HUD _hud;
   public void SetHumanHealth(int health) { _hud.SetHumanHealth(health); }
}
