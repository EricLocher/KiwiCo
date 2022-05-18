using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public Transform target;
    [HideInInspector] public List<AttackCone> attacks = new List<AttackCone>();

    void Attack()
    {
        int randomIndex = Random.Range(0, attacks.Count);

        //Call on AttackCone TargetInCone.
    }

    private void Update()
    {
        foreach (AttackCone attack in attacks) {
            if (attack.TargetInCone(target)) {
                Debug.Log("In Zone");
            }
        }
    }


    public void AddAttack(AttackCone attack)
    {
        attacks.Add(attack);
    }

    public void RemoveAttack(AttackCone attack)
    {
        attacks.Remove(attack);
    }

}
