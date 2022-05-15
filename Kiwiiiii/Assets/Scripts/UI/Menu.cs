using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject settings;

    void Start()
    {
        Save.instance.LoadAll();
    }

    public void ToggleSettings()
    {
        AudioManager.instance.PlayOnce("Menu Button");
        if (settings.activeInHierarchy == false)
        {
            settings.SetActive(true);
        }
        else
        {
            settings.SetActive(false);
        }
    }
}
