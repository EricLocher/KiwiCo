using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampSurprise : EnemySurprise
{
    private Animator animator;

    public override void EnterSurprise()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("alert");
    }

    public override void ActiveSurprise()
    {
        return;
    }

    public override void ExitSurprise()
    {
        animator.ResetTrigger("alert");
    }
}
