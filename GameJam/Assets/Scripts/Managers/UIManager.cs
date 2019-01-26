using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
   public Canvas BaseCanvas;

   public GameObject IndicatorPanel;
   public GameObject WinPanel;
   public GameObject LosePanel;
   public GameObject NextScenePanel;

   private static UIManager _instance;
   
   public void OpenWinPanel() {
      WinPanel.SetActive(true);
   }
   
   public void OpenLosePanel() {
      LosePanel.SetActive(true);
   }
}
