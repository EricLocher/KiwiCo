using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    int buildIndex = 0;
    GameStates state;

    void OnEnable()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        state = GameController.gameState;
        if (state == GameStates.Menu)
        {
            AudioManager.instance.PauseSound("Game Music");
            AudioManager.instance.Play("Menu Music");
            return;
        }

        AudioManager.instance.PauseSound("Menu Music");

        if (buildIndex == 3) {;
            AudioManager.instance.PauseSound("Game Music");
            AudioManager.instance.Play("Boss Music");
            return;
        }

        AudioManager.instance.Play("Game Music");
    }
}
