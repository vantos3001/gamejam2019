using UnityEngine;
using UnityEngine.SceneManagement;

public static class SwapSceneController
{
    public static void SwapScene() {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Preloader") {
            SceneManager.LoadScene("Worm Plus Meat Scene");
        }
    }
}
