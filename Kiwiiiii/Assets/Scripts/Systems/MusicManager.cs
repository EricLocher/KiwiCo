using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    int buildIndex = 0;
    GameStates state;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        state = GameController.gameState;
        //state shit not working properly
        if (state == GameStates.Menu)
        {
            AudioManager.instance.PauseSound("Boss Music");
            AudioManager.instance.PauseSound("Game Music");
            AudioManager.instance.Play("Menu Music");
            return;
        }

        AudioManager.instance.PauseSound("Boss Music");
        AudioManager.instance.PauseSound("Menu Music");

        if (buildIndex == 3) 
        {
            Debug.Log("test");
            AudioManager.instance.PauseSound("Game Music");
            AudioManager.instance.Play("Boss Music");
            return;
        }

        if(state != GameStates.Menu && buildIndex != 3 && buildIndex != 2)
        {
            AudioManager.instance.PauseSound("Boss Music");
            AudioManager.instance.PauseSound("Game Music2");
            AudioManager.instance.Play("Game Music");
        }

        if (state != GameStates.Menu && buildIndex != 3 && buildIndex != 1)
        {
            AudioManager.instance.PauseSound("Boss Music");
            AudioManager.instance.PauseSound("Game Music");
            AudioManager.instance.Play("Game Music2");
        }
    }
}
