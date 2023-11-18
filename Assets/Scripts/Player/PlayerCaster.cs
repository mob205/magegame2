using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCaster : MonoBehaviour
{
    [SerializeField] private string[] _inputs;

    public static event Action OnAbilityChange;

    public static Ability[] Abilities { get; private set; } = new Ability[7];
    private PlayerInput _playerInput;

    private bool _isCasting;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        var children = GetComponentsInChildren<Ability>();
        Array.Copy(children, Abilities, children.Length);
        OnAbilityChange?.Invoke();
    }
    private void Update()
    {
        CastAbilities();
    }
    // Checks for ability inputs and casts spells if needed
    private void CastAbilities()
    {
        for(int i = 0; i < Abilities.Length; i++)
        {
            if(!Abilities[i]) { continue; }
            if (_playerInput.actions[_inputs[i]].IsPressed() && Abilities[i].CanCast(_isCasting))
            {
                Abilities[i].CastAbility(Mouse.Position);
            } 
            else if (_playerInput.actions[_inputs[i]].WasReleasedThisFrame())
            {
                Abilities[i].StopAbility();
            }
        }
    }
    // Adds an ability to the first empty slot, if there is one
    public void AddAbility(Ability ability)
    {
        for(int i = 0; i < Abilities.Length; i++)
        {
            if(Abilities[i] == null)
            {
                Abilities[i] = Instantiate(ability, transform);
                OnAbilityChange?.Invoke();
                break;
            }
        }
    }
    // Removes an ability at the index, if the index is valid
    public void RemoveAbility(int index)
    {
        if(index < Abilities.Length)
        {
            Destroy(Abilities[index]);
            Abilities[index] = null;
            OnAbilityChange?.Invoke();
        }
    }
}

