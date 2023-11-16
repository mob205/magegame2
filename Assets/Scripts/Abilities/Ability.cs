using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] protected float _baseCooldown;
    [SerializeField] protected Sprite _icon;

    public float Cooldown
    {
        get { return _baseCooldown; }
    }
    public Sprite Icon
    {
        get { return _icon; }
    }
    protected bool _isOnCooldown;
    protected float _remainingCooldown;
    protected virtual void Update()
    {
        UpdateCooldown();
    }
    protected virtual void UpdateCooldown()
    {
        if (_isOnCooldown)
        {
            _remainingCooldown -= Time.deltaTime;
        }
        if(_remainingCooldown <= 0)
        {
            _isOnCooldown = false;
        }
    }
    public virtual void CastAbility(Transform target)
    {
        // Cast ability
    }
    public virtual void StopAbility()
    {
        // Stop ability
    }
    protected void StartCooldown()
    {
        _isOnCooldown = true;
        _remainingCooldown = _baseCooldown;
    }
    public virtual bool CanCast(bool isCasting)
    {
        return !_isOnCooldown;
    }
}
