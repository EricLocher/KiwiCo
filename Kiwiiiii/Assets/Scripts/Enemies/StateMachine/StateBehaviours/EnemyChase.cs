using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyChase : MonoBehaviour
{
    public abstract void EnterChase();
    public abstract void ActiveChase();
    public abstract void ExitChase();
}