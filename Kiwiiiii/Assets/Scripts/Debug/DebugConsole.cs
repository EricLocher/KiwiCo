using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class DebugConsole : MonoBehaviour
{
    static DebugConsole _instance;
    public static DebugConsole Instance { get { return _instance; } }

    [Header("Console Commands")]
    [SerializeField] string prefix = "";
    [SerializeField] DebugCommand[] commands = new DebugCommand[0];

    [Header("UI/Input")]
    [SerializeField] GameObject devCanvas;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text textField;
    [SerializeField] InputAction openConsole;

    bool showConsole;
    Console Console;

    void Awake()
    {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this.gameObject);
        }

        openConsole.Enable();
        openConsole.performed += ctx => OnToggleConsole();

        Console = new Console(prefix, commands);
    }

    public void ProcessCommand(string inputValue)
    {
        Console.ProcessCommand(inputValue);
    }

    void OnToggleConsole()
    {
        showConsole = !showConsole;
        devCanvas.SetActive(showConsole);
        GameController.PauseGame(showConsole);
        inputField.ActivateInputField();
    }

    public void TextInput(string text)
    {
        inputField.ActivateInputField();
        if(text == "") { return; }

        print(text);
        inputField.text = "";
        textField.text += "\n" + text;

        ProcessCommand(text);
    }


}


