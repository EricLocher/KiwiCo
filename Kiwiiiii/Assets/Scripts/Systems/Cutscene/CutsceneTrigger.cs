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
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            StartCinematic();
        }
    }

    private void StartCinematic()
    {
        CutSceneCamera.enabled = true;
        animBars.SetBool("Blackbars", true);
        animCam.SetBool("Cinematic", true);

        StartCoroutine(cutsceneController.StartText());

        StartCoroutine(EndCinematic());
    }

    private IEnumerator EndCinematic()
    {
        yield return new WaitForSeconds(10f);

        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        foreach (Camera cam in FindObjectsOfType<Camera>())
        {
            cam.enabled = true;
        }

        CutSceneCamera.enabled = false;
        animBars.SetBool("Blackbars", false);
        animCam.SetBool("Cinematic", false);
        cutsceneController.isOpen = false; ;

    }

}
