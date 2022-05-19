using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] PlayerController player;

    void Update()
    {
        fillImage.fillAmount = (player.stats.health / 300f);
    }
}