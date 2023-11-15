using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedEffect : StatusEffect
{
    private readonly PlayerController _player; 
    public StunnedEffect(ScriptableEffect effect, GameObject target) : base(effect, target)
    {
        _player = target.GetComponent<PlayerController>();
    }
    protected override void ApplyEffect()
    {
        if (_player)
        {
            _player.ToggleStun(true);
        }
    }
    public override void End()
    {
        if (_player)
        {
            _player.ToggleStun(false);
        }
        EffectStacks = 0;
    }
}
