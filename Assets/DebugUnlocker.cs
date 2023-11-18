using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUnlocker : MonoBehaviour
{
    [SerializeField] private Ability[] _abilities;
    private void Start()
    {
        foreach(var ability in _abilities)
        {
            SpellUnlocker.UnlockSpell(ability);
        }
    }
}
