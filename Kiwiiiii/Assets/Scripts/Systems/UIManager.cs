using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    List<GameObject> uiElements = new List<GameObject>();

    [SerializeField] GameObject pauseScreen;

    void Start()
    {
        foreach (Transform child in transform)
            uiElements.Add(child.gameObject);
    }

    public void OnPause(GameStates state)
    {
        if (state == GameStates.Paused)
        {
            foreach (GameObject obj in uiElements)
                obj.SetActive(false);

            pauseScreen.SetActive(true);
        }
        else if (state != GameStates.Paused)
        {
            foreach (GameObject obj in uiElements)
                obj.SetActive(true);

            pauseScreen.SetActive(false);
        }
    }

    void OnEnable() => GameController.onStateChange += OnPause;
    void OnDisable() => GameController.onStateChange -= OnPause;

}
