using UnityEngine;
using System;
using System.Threading.Tasks;

[Serializable]
public class CutsceneEvent : GameEvent
{
    [SerializeField] GameObject cameraCenter;
    [SerializeField] AnimationClip anim;
    [SerializeField] Animator animator;
    [SerializeField] Animator animBars;
    [SerializeField] GameObject player;
    CutsceneController cutsceneController;
    [SerializeField, TextArea(3, 20)]
    string sentence;

    Vector3 camStartPos, camStartRot;

    public override void Init()
    {
        cutsceneController = cameraCenter.GetComponent<CutsceneController>();
    }

    public override void StartEvent(EventZone zone)
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        FindObjectOfType<Canvas>().GetComponent<UIManager>().HideAllUIExceptCutscene();
        cameraCenter.transform.GetChild(0).GetComponent<CameraController>().IsCinematic = true;

        cutsceneController.InitStart(sentence);

        camStartPos = cameraCenter.transform.position;
        camStartRot = cameraCenter.transform.eulerAngles;

        WaitForCinematic();
    }

    private async void WaitForCinematic()
    {
        animator.enabled = true;
        animator.Play(anim.name);
        animBars.SetTrigger("Open");
        await Task.Delay((int)(anim.length * 1000f));
        CompletedEvent();
    }

    public override void CompletedEvent()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        FindObjectOfType<Canvas>().GetComponent<UIManager>().ShowAllUIElements();
        cameraCenter.transform.GetChild(0).GetComponent<CameraController>().IsCinematic = false;
        animator.enabled = false;
        animBars.SetTrigger("Close");

        cameraCenter.transform.position = camStartPos;
        cameraCenter.transform.eulerAngles = camStartRot;

        cutsceneController.SetAnim(false);
    }
}
