using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject settings;
    [SerializeField] GameObject continueBtn;

    void Awake()
    {
        var savedScene = Save.instance.CheckPreviousSave();
        if(savedScene == 0) { continueBtn.SetActive(false); return; }
        continueBtn.SetActive(true);
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
