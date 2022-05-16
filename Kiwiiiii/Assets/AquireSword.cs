using UnityEngine;

public class AquireSword : MonoBehaviour
{
    [SerializeField] ControlWeapon sword;
    [SerializeField] PlayerInput controls;

    void Start()
    {
        if (Save.instance.aquiredSword)
        {
            controls.EnableSheathWeapon();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Character"))
        {
            Save.instance.aquiredSword = true;
            sword.SheathWeapon();
            controls.EnableSheathWeapon();
            Destroy(gameObject);
        }
    }

}
