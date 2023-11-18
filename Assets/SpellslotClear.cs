using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellslotClear : Selectable
{
    private PlayerCaster _player;
    private void Start()
    {
        _player = PlayerInstance.Instance.GetComponent<PlayerCaster>();
    }
    public override void Select()
    {
        _player.RemoveAbility(transform.GetSiblingIndex());
    }
}
