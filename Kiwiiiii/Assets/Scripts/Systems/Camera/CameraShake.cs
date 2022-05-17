using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    Vector3 _originalPos;
    float timeAtCurrentFrame;
    float timeAtLastFrame;
    float fakeDelta;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        timeAtCurrentFrame = Time.realtimeSinceStartup;
        fakeDelta = timeAtCurrentFrame - timeAtLastFrame;
        timeAtLastFrame = timeAtCurrentFrame;
    }

    public static void Shake(float duration, float amount)
    {
        instance._originalPos = instance.gameObject.transform.localPosition;
        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.cShake(duration, amount));
    }

    public IEnumerator cShake(float duration, float amount)
    {
        while (duration > 0)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= fakeDelta;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }
}
