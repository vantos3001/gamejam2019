using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapSceneController : MonoBehaviour
{
    public void SwapScene() {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Preloader") {
            SceneManager.LoadScene("Worm Plus Meat Scene");
        }
    }
}
