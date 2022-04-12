using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameTestingCheats : MonoBehaviour
{
    private void Update()
    {
        RestartScene();
    }
    private void RestartScene()
    {
        if (Keyboard.current.rKey.isPressed)
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }     
    }
}
