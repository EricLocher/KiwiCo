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

    public bool pause;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        pauseGame.Enable();

        pauseGame.performed += ctx => PauseGame();
    }

    void Update()
    {
        if (gameState == GameStates.Playing) { pause = true; } else { pause = false; }
    }

    public void PauseGame()
    {
        if (Instance.pause)
        {
            //Time.timeScale = 0;
            gameState = GameStates.Paused;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            gameState = GameStates.Playing;
            onStateChange?.Invoke(gameState);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        onStateChange?.Invoke(gameState);
    }

    public static void Quit()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}




public enum GameStates
{
    Paused,
    Playing
}
