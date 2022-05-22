using UnityEngine;

public class AquireSword : MonoBehaviour
{

    [SerializeField] ControlWeapon sword;
    [SerializeField] PlayerInput controls;

    void Start()
    {
        if(Save.instance.aquiredSword)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Character"))
        {
            Save.instance.aquiredSword = true;
            sword.SheathWeapon();
            Save.instance.SaveAll();
            Destroy(gameObject);
        }
    }
}
