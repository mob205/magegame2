using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SpellUnlocker
{
    private static readonly List<Ability> _unlockedAbilities = new();
    public static void UnlockSpell(Ability ability)
    {
        _unlockedAbilities.Add(ability);
    }
    public static Ability[] GetUnlockedAbilities()
    {
        return _unlockedAbilities.ToArray();
    }
}
