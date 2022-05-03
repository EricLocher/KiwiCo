using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    [HideInInspector]
    public int sceneIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            SaveAll();
            Debug.Log("Saving");
        }
    }

    public void SaveAll()
    {
        SaveData.Save(this);
    }

    public void LoadAll()
    {
        GameData data = SaveData.Load();
        sceneIndex = data.sceneIndex;
        SceneManager.LoadScene(sceneIndex);
        AudioManager.instance.PauseSound("Menu Music");
        AudioManager.instance.Play("Game Music");
    }
}
