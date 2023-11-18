using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] protected Sprite _icon;
    [SerializeField] private string _name;
    [TextArea][SerializeField] private string _description;
    [Header("Gameplay")]
    [SerializeField] protected float _baseCooldown;
    

    public float BaseCooldown
    {
        get { return _baseCooldown; }
    }
    public Sprite Icon
    {
        get { return _icon; }
    }
    public string Name
    {
        get { return _name; }
    }
    public float RemainingCooldown { get; private set; }

    protected bool _isOnCooldown;
    protected virtual void Update()
    {
        UpdateCooldown();
    }
    protected virtual void UpdateCooldown()
    {
        if (_isOnCooldown)
        {
            RemainingCooldown -= Time.deltaTime;
        }
        if(RemainingCooldown <= 0)
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
        RemainingCooldown = _baseCooldown;
    }
    public virtual bool CanCast(bool isCasting)
    {
        return !_isOnCooldown;
    }
}
