using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugConsole : MonoBehaviour
{
    bool show;

    public void OnToggleConsole(InputAction.CallbackContext value)
    {
        show = !show;
        GameController.PauseGame(show);
    }




}
