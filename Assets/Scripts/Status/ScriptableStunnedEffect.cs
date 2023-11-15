using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableStunnedEffect : ScriptableEffect
{
    public override StatusEffect InitializeEffect(GameObject target)
    {
        return new StunnedEffect(this, target);
    }
}
