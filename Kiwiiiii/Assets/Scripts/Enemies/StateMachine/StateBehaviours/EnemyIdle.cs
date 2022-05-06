using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyIdle : MonoBehaviour
{
    public abstract void EnterIdle();
    public abstract void ActiveIdle();
    public abstract void ExitIdle();
}
