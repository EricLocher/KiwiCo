using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Ability : ScriptableObject
{
    [SerializeField] InputAction action;
    public new string name = "NoName";
    public float coolDownTime = 0;
    public int maxAmount = 1;
    public int currentAmount;

    public float timer
    {
        get {
            if (timers.Count == 0) { return 0; }
            return timers[0];
        }
    }

    List<float> timers;

    public void Init(Rigidbody rb)
    {
        action.Enable();
        action.performed += ctx => Activate(ctx, rb);
        currentAmount = maxAmount;
        timers = new List<float>();
    }

    public virtual void Activate(InputAction.CallbackContext ctx, Rigidbody rb)
    {
        if (currentAmount <= 0) { return; }

        DoAbility(rb);

        currentAmount--;

        timers.Add(coolDownTime);
    }

    public abstract void DoAbility(Rigidbody rb);

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

