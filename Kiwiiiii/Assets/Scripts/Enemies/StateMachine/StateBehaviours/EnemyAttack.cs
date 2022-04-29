using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    public abstract void EnterAttack();
    public abstract void ActiveAttack();
    public abstract void ExitAttack();
}
