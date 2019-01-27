using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapSceneController : MonoBehaviour
{
    public static void SwapScene() {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Preloader") {
            SceneManager.LoadScene("Worm Plus Meat Scene");
        }
    }

    public static void RestartGame() {
        SceneManager.LoadScene("ActualyLastTestLevelScene");
    }

    public static void WatchVideo(){
        SceneManager.LoadScene("Video Player", LoadSceneMode.Single);
    }
}
