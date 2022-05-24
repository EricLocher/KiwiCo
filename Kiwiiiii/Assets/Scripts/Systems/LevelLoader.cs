using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadLoading(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync("Loading");
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
        LoadScene(sceneName);
        
    }

    public async void LoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        do
        {
            await Task.Delay(100);
            GameObject.FindGameObjectWithTag("Loading").GetComponent<Image>().fillAmount = scene.progress;

        } while (scene.progress < 0.9f);

        scene.allowSceneActivation = true;
    }
}
