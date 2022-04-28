using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public static GameStates gameState = GameStates.Playing;

    [SerializeField] InputAction pauseGame;

    public delegate void ChangeHandler(GameStates state);
    public static event ChangeHandler onStateChange;

    bool pause;

    void Awake()
    {
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        pauseGame.Enable();

        pauseGame.performed += ctx => PauseGame(pause);
    }

    void Update()
    {
        if (gameState == GameStates.Playing) { pause = true; } else { pause = false; }
    }

    public static void PauseGame(bool shouldPause)
    {
        if (shouldPause)
        {
            Time.timeScale = 0;
            gameState = GameStates.Paused;
        }
        else
        {
            Time.timeScale = 1;
            gameState = GameStates.Playing;
            onStateChange?.Invoke(gameState);
        }
        onStateChange?.Invoke(gameState);
    }

    public static void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}




public enum GameStates
{
    Paused,
    Playing
}
