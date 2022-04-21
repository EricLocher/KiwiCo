using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttacks : MonoBehaviour
{
    void Attack()
    {
        Debug.Log("Attacking!!!");
    }

    private void OnEnable()
    {
        AttackState.testAttack += Attack;
    }

    private void OnDisable()
    {
        AttackState.testAttack -= Attack;
    }
}
