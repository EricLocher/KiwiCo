using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public static GameStates gameStates = GameStates.Playing;

    private void Awake()
    {
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public static void PauseGame(bool shouldPause)
    {
        if(shouldPause) {
            Time.timeScale = 0;
            gameStates = GameStates.Paused;
        }
        else if(gameStates == GameStates.Paused){
            Time.timeScale = 1;
            gameStates = GameStates.Playing;
        }
    }


}

public enum GameStates
{
    Paused,
    Playing
}
