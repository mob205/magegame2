using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashDuration;
    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }
    private void Update()
    {
        UpdateCooldown();
    }
    public override void CastAbility(Transform target)
    {
        StartCoroutine(ApplyDash());
        StartCooldown();
    }
    private IEnumerator ApplyDash()
    {
        var dir = new Vector2(_player.GetMovementDir(), 0);
        _player.ResetVelocity();
        _player.ToggleStun();
        _player.ToggleForce();
        _player.AddExternalVelocity(dir, _dashForce);

        yield return new WaitForSeconds(_dashDuration);

        _player.ResetVelocity();
        _player.ToggleStun();
        _player.ToggleForce();
    }
    public override bool CanCast(bool isCasting)
    {
        return base.CanCast(isCasting) && _player.GetMovementDir() != 0;
    }
}
