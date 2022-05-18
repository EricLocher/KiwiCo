using UnityEngine;

public class ZoneTP : MonoBehaviour
{
    [SerializeField] Transform targetPos;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Character"))
        {
            foreach (Transform child in collision.gameObject.transform.parent.transform)
            {
                child.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                child.gameObject.transform.position = targetPos.position;
            }
        }
    }
}
