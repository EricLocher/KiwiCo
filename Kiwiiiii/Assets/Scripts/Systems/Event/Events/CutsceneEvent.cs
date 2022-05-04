using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;

[Serializable]
public class CutsceneEvent : GameEvent
{
    Camera camera;
    [SerializeField] AnimationClip anim;
    [SerializeField] Animator animation;
    [SerializeField] Animator animBars;
    [SerializeField] GameObject player;
    CutsceneController cutsceneController;

    Vector3 camStartPos, camStartRot;

    public override void Init()
    {
        camera = Camera.main;
        cutsceneController = camera.GetComponent<CutsceneController>();
    }

    public override void StartEvent(EventZone zone)
    {
        cutsceneController.InitStart();

        camStartPos = camera.transform.position;
        camStartRot = camera.transform.eulerAngles;

        WaitForCinematic();


    }

    private async void WaitForCinematic()
    {
        animation.enabled = true;
        animation.Play(anim.name);
        animBars.SetTrigger("Open");
        await Task.Delay((int)(anim.length * 1000f));
        CompletedEvent();
    }

    public override void CompletedEvent()
    {
        animation.enabled = false;
        animBars.SetTrigger("Close");
        //animBars.enabled = false;

        camera.transform.position = camStartPos;
        camera.transform.eulerAngles = camStartRot;

        cutsceneController.isOpen = false;
    }
}
