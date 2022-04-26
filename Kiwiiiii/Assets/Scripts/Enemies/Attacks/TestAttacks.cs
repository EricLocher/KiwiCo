using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttacks : MonoBehaviour
{
    public GameObject damageArea;

    private Animator animator;
    private Color damageAreaColor;
    private SphereCollider damageCollider;

    private float drawFrequency = 2;
    private float timer = 2.5f;

    private bool fadeOut;
    private bool firstAttack;
    private bool doneAttacking;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        damageAreaColor = damageArea.GetComponent<SpriteRenderer>().color;
        damageCollider = damageArea.GetComponent<SphereCollider>();
    }

    void EnterAttack()
    {
        //TODO: New color setting damageArea.a to 0
        fadeOut = true;
        firstAttack = false;
        return;
    }

    void ActiveAttack()
    {
        Invoke(nameof(DrawDamageArea), drawFrequency);
    }

    void ExitAttack()
    {
        return;
    }

    void DrawDamageArea()
    {
        if (fadeOut)
        {
            if (firstAttack)
            {
                FadeIn();
                firstAttack = false;
            }

            Invoke(nameof(FadeIn), 2);
        }

        if (damageAreaColor.a >= 1)
        {
            fadeOut = false;
            FadeOut();
            Invoke(nameof(SpinAttack), 1);
        }

        if (doneAttacking)
        {
            fadeOut = true;
            doneAttacking = false;
            timer = 2.5f;
        }

    }

    void SpinAttack()
    {
        animator.SetBool("attacking", true);
        damageCollider.enabled = true;
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            damageCollider.enabled = false;
            animator.SetBool("attacking", false);
            doneAttacking = true;
        }
    }

    void FadeIn()
    {
        /*
        float fadeamount = color.a + (fadespeed * dt)
        newColor = new Color (r, g, b, fadeamount)
        damageAreaColor = newColor
        */
    }

    void FadeOut()
    {
        /* float fadeamount = color.a - (fadespeed * dt)
        newColor = new Color (r, g, b, fadeamount)
        damageAreaColor = newColor
        */
    }

    private void OnEnable()
    {
        AttackState.enterAttack += EnterAttack;
        AttackState.exitAttack += ExitAttack;
    }

    private void OnDisable()
    {
        AttackState.enterAttack -= EnterAttack;
        AttackState.exitAttack -= ExitAttack;
    }
}
