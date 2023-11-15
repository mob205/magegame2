using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableEffect : ScriptableObject
{
    public float Duration;
    public bool IsDurationStacked;
    public bool IsRefreshStacked;
    public bool IsEffectStacked;
    public abstract StatusEffect InitializeEffect(GameObject target);
}
