using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            Save.instance.aquiredSword = true;
            AudioManager.instance.PlayOnce("PlayerEnterPortal");
            string scene = NameFromIndex(SceneManager.GetActiveScene().buildIndex + 1);
            LevelLoader.Instance.LoadLoading(scene);
            //ta brt
        }
    }

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

}
