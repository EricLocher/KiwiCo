using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class DebugConsole : MonoBehaviour
{
    [SerializeField] GameObject devCanvas;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text textField;
    [SerializeField] InputAction openConsole;
    bool show;

    List<string> history = new List<string>();

    void Awake()
    {
        openConsole.Enable();
        openConsole.performed += ctx => OnToggleConsole();
    }

    void OnToggleConsole()
    {
        show = !show;
        devCanvas.SetActive(show);
        GameController.PauseGame(show);
        inputField.ActivateInputField();
    }

    public void TextInput(string text)
    {
        inputField.ActivateInputField();
        if(text == "") { return; }

        print(text);
        inputField.text = "";
        textField.text += "\n" + text;
    }


}
