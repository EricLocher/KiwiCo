using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttack : EnemyAttack
{
    [SerializeField]
    Enemy enemy;

    //public GameObject damageArea;

    private Animator animator;
    //private SpriteRenderer damageAreaSprite;
    //private Color fadeColor;
    //private Color invisible;
    private SphereCollider spinCollider;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //damageAreaSprite = damageArea.GetComponent<SpriteRenderer>();
        //damageCollider = damageArea.GetComponent<SphereCollider>();

        //invisible = new Color(damageAreaSprite.color.r, damageAreaSprite.color.g, damageAreaSprite.color.b, 0);
    }

    public override void EnterAttack()
    {
        StartCoroutine(Attack());
        //damageAreaSprite.color = invisible;
        //StartCoroutine(FadeIn(2));
        return;
    }

    IEnumerator Attack()
    {
        //TODO: Make spin start slow and go faster
        animator.SetTrigger("spin");
        spinCollider.enabled = true;

        yield return new WaitForSeconds(2);
        spinCollider.enabled = false;
        animator.ResetTrigger("spin");

        //damageAreaSprite.color = invisible;

        enemy.stateMachine.ChangeState(EnemyStates.Chase);
    }

    //IEnumerator FadeIn(float time)
    //{
    //    for (float fadeamount = 0; fadeamount <= 1; fadeamount += time * Time.deltaTime)
    //    {
    //        fadeColor = new Color(damageAreaSprite.color.r, damageAreaSprite.color.g, damageAreaSprite.color.b, fadeamount);
    //        damageAreaSprite.color = fadeColor;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    StartCoroutine(FadeOut(2));
    //    yield return null;
    //}

    //IEnumerator FadeOut(float time)
    //{
    //    for (float fadeamount = 1; fadeamount >= 0; fadeamount -= time * Time.deltaTime)
    //    {
    //        fadeColor = new Color(damageAreaSprite.color.r, damageAreaSprite.color.g, damageAreaSprite.color.b, fadeamount);
    //        damageAreaSprite.color = fadeColor;
    //        yield return new WaitForEndOfFrame();
    //    }

    //    StartCoroutine(Attack());
    //    yield return null;
    //}

    public override void ActiveAttack()
    {
        return;
    }

    public override void ExitAttack()
    {
        return;
    }
}
