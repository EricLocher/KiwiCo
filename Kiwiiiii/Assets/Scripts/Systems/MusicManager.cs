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
            //AudioManager.instance.PauseAllSound();
            //AudioManager.instance.Play("Menu Music");
        }
        //AudioManager.instance.PauseAllSound();
        //AudioManager.instance.Play("Game Music");
    }
}
