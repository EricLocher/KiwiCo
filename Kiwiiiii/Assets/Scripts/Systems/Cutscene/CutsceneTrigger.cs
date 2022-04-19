using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public Camera CutSceneCamera;
    private CutsceneController cutsceneController;
    public Animator animCam;
    public Animator animBars;

    void Awake()
    {
        cutsceneController = CutSceneCamera.GetComponent<CutsceneController>();
        CutSceneCamera.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (Camera cam in FindObjectsOfType<Camera>())
            {
                cam.enabled = false;
            }

            CutSceneCamera.enabled = true;

            StartCoroutine(cutsceneController.StartText());
        }
    }

}
