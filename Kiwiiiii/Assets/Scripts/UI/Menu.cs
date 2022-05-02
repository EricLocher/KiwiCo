using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject settings;

    public void ToggleSettings()
    {
        if(settings.activeInHierarchy == false)
        {
            settings.SetActive(true);
        }
        else
        {
            settings.SetActive(false);
        }
    }
}
