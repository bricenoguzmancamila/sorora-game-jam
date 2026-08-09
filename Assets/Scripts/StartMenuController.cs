using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour{
    public string gameSceneName="delta waves";
    public void StartGame(){
        SceneManager.LoadScene(gameSceneName);
    }
}