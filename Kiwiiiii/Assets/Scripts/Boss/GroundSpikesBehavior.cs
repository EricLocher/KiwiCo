using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;
using UnityEngine.Animations;


public class GroundSpikesBehavior : MonoBehaviour
{
    [SerializeField] FOV fov;
    [SerializeField] DecalProjector projector;
    [SerializeField] VisualEffect vfx;
    [SerializeField] AnimationCurve curve;

    Material mat;

    public void Attack(float time, float damage, Transform target)
    {
        mat = projector.material;
        vfx.Stop();
        mat.SetFloat("_EmissiveExposureWeight", 0.99f);
        StartCoroutine(FadeInAndAttack(time, damage, target));
    }

    IEnumerator FadeInAndAttack(float time, float damage, Transform target)
    {
        float timeElapsed = 0;

        while(timeElapsed < time) {
            yield return new WaitForEndOfFrame();
            float newVal = curve.Evaluate(timeElapsed/time);
            mat.SetFloat("_EmissiveExposureWeight", newVal);
            timeElapsed += Time.deltaTime;
        }

        projector.enabled = false;

        vfx.Play();
        CameraShake.Shake(0.1f, 0.3f);
        if (fov.TargetInView(target)) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().TakeDamage(damage);
        }

        Destroy(gameObject, 2f);

        yield return null;
    }

    

}
