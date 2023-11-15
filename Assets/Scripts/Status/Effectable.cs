using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Effectable : MonoBehaviour
{
    [SerializeField] private ScriptableEffect[] startingEffects;
    private Dictionary<ScriptableEffect, StatusEffect> _effects = new();

    private void Start()
    {
        foreach(var effect in startingEffects)
        {
            AddEffect(effect.InitializeEffect(gameObject));
        }
    }
    public void AddEffect(StatusEffect effect)
    {
        // Effect is in the dictionary, but is inactive.
        // So, just activate it
        if (_effects.ContainsKey(effect.Effect))
        {
            _effects[effect.Effect].Activate();
        }
        // Effect is not in the dictionary
        // So, add it in the dictionary and activate it
        else
        {
            _effects.Add(effect.Effect, effect);
            effect.Activate();
        }
    }
    public StatusEffect[] GetEffects() => _effects.Values.ToArray();
}
