using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    int buildIndex = 0;
    void OnEnable()
    {
        buildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (buildIndex == 0 || buildIndex == 4)
        {
            AudioManager.instance.PauseSound("Game Music");
            AudioManager.instance.Play("Menu Music");
            return;
        }
        AudioManager.instance.PauseSound("Menu Music");
        AudioManager.instance.Play("Game Music");
    }
}
