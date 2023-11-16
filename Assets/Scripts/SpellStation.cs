using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStation : Interactable
{
    private AbilitySelector _selector;
    protected override void Awake()
    {
        base.Awake();
        _selector = AbilitySelector.Instance;
    }
    protected override void Interact()
    {
        _selector.Activate();
    }
}
