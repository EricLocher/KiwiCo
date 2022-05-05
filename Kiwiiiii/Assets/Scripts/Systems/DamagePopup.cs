using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePopup : MonoBehaviour
{

    void FixedUpdate()
    {
        float speed = Random.Range(0.1f, 0.5f) * Time.fixedDeltaTime;

        float height = Random.Range(0.5f, 5f);

        var startPos = transform.position;

        var newPos = new Vector3(startPos.x, startPos.y + height, startPos.z);

        transform.position = Vector3.Lerp(startPos, newPos, speed);
        transform.LookAt(Camera.main.transform);
    }

    public void PopupDamage(int damage)
    {
        TMP_Text popUp = GetComponent<TMP_Text>();
        popUp.text = "" + damage;

        StartCoroutine(FadeTextToZeroAlpha(2f, popUp));
        Destroy(gameObject, 2f);
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}
