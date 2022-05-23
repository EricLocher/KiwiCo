using UnityEngine;

public class RayBehavior : MonoBehaviour
{
    [SerializeField] public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            var playerController = other.gameObject.transform.parent.GetComponent<PlayerController>();
            playerController.TakeDamage(damage);
            var dmg = Instantiate(playerController.hit, other.gameObject.transform);
            Destroy(dmg, 0.3f);
        }
    }
}