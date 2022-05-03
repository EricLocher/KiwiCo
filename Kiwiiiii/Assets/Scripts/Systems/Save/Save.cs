using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public int sceneIndex;

    public void SaveAll()
    {
        SaveData.Save(this);
    }

    public void LoadAll()
    {
        GameData data = SaveData.Load();
        sceneIndex = data.sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
