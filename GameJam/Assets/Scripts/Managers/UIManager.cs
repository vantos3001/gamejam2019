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

   private void Start() {
      _hud = HUDGameObject.GetComponent<HUD>();
   }

   #region Show and Hide Methods
   public void ShowHUD() {
      HUDGameObject.gameObject.SetActive(true);
   }

   public void HideHUD() {
      HUDGameObject.gameObject.SetActive(false);
   }
   
   public void ShowWinPanel() {
      WinPanel.SetActive(true);
   }

   public void HideWinPanel() {
      WinPanel.SetActive(false);
   }
   
   public void ShowLosePanel() {
      LosePanel.SetActive(true);
   }

   public void HideLosePanel() {
      LosePanel.SetActive(false);
   }

   public void ShowNextScenePanel() {
      NextScenePanel.SetActive(true);
   }

   public void HideNextScenePanel() {
      NextScenePanel.SetActive(false);
   }

   public void HideAllPanels() {
      HideHUD();
      HideWinPanel();
      HideLosePanel();
      HideNextScenePanel();
   }
   #endregion

   #region HUD

   private HUD _hud;
   public void SetScoreText(int score) {
      _hud.SetScoreText(score);
   }

   public void SetHumanHealth(int health) {
      _hud.SetHumanHealth(health);
   }
   

   #endregion

}
