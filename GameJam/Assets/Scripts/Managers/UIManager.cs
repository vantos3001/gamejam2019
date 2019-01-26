using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
   public Canvas BaseCanvas;

   public GameObject HUD;
   public GameObject WinPanel;
   public GameObject LosePanel;
   public GameObject NextScenePanel;

   private static UIManager _instance;


   #region Show and Hide Methods
   public void ShowHUD() {
      HUD.gameObject.SetActive(true);
   }

   public void HideHUD() {
      HUD.gameObject.SetActive(false);
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

}
