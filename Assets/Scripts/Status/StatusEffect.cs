using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    public ScriptableEffect Effect { get; }
    public bool IsFinished;

    protected float Duration;
    protected int EffectStacks;
    protected readonly GameObject _target;

    // Constructor
    public StatusEffect(ScriptableEffect effect, GameObject target)
    {
        Effect = effect;
        _target = target;
    }
    // Updates the cooldown each tick, which is called by the Effectable
    public virtual void Tick(float delta)
    {
        Duration -= delta;
        if(Duration <= 0)
        {
            End();
            IsFinished = true;
        }
    }
    // Activates the effect
    public void Activate()
    {
        // Apply the effect if it is effect stackable or inactive
        if(Effect.IsEffectStacked || Duration <= 0)
        {
            ApplyEffect();
            EffectStacks++;
        }
        // Extend its duration if the effect is duration stackable or inactive
        if(Effect.IsDurationStacked || Duration <= 0)
        {
            Duration += Effect.Duration;
        } 
        // Reset duration instead of extending it
        else if (Effect.IsRefreshStacked)
        {
            Duration = Effect.Duration;
        }
    }
    protected abstract void ApplyEffect();
    public abstract void End();
}
