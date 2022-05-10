using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Ability : ScriptableObject
{
    [Header("Icon")]
    public Sprite icon;
    [Header("Input")]
    public InputAction action;
    [Header("Ability Settings")]
    public new string name = "NoName";
    public float coolDownTime = 0;
    public int maxAmount = 1;
    public int currentAmount;

    protected PlayerMovement movement;
    protected ControlWeapon sword;
    public float timer
    {
        get {
            if (timers.Count == 0) { return 0; }
            return timers[0];
        }
    }

    List<float> timers;

    public void Init(PlayerMovement movement, ControlWeapon sword)
    {
        action.Enable();

        this.movement = movement;
        this.sword = sword;

        action.started += ctx => StartedAbility(ctx);
        action.performed += ctx => Activate(ctx);
        action.canceled += ctx => CanceledAbility(ctx);

        currentAmount = maxAmount;
        timers = new List<float>();
        InitAbility();
    }

    protected virtual void InitAbility() { }

    public virtual void Activate(InputAction.CallbackContext ctx)
    {
        if (currentAmount <= 0) { return; }

        DoAbility();

        currentAmount--;

        timers.Add(coolDownTime);
    }

    public virtual void StartedAbility(InputAction.CallbackContext ctx) { }
    public virtual void CanceledAbility(InputAction.CallbackContext ctx) { }
    public abstract void DoAbility();

    public virtual void UpdateAbility(float dt)
    {
        for (int i = 0; i < timers.Count; i++) {
            timers[i] -= dt;
            if (timers[i] <= 0) {
                currentAmount++;
                timers.RemoveAt(i);
                i--;
            }
        }
    }

}

