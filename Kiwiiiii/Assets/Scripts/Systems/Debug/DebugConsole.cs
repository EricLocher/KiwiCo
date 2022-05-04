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
    [SerializeField] DebugCommand[] commands = new DebugCommand[0];

    [Header("UI/Input")]
    [SerializeField] GameObject devCanvas;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text textField;
    [SerializeField] InputAction openConsole;

    string prefix = "/";
    bool showConsole;
    Console Console;

    void Awake()
    {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        openConsole.Enable();
        openConsole.performed += ctx => OnToggleConsole();

        Console = new Console(prefix, commands);
    }

    public string ProcessCommand(string inputValue)
    {
       return Console.ProcessCommand(inputValue);
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

        inputField.text = "";
        textField.text += $"\n{text}";

        string retVal = ProcessCommand(text);
        if (retVal != "")
        textField.text += $"\n<color=#2c8cdb>{retVal}</color>";

    }

    void OnDestroy()
    {
        openConsole.Disable();
        openConsole.performed -= ctx => OnToggleConsole();
    }
}


