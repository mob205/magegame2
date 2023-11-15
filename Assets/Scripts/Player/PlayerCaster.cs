using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCaster : MonoBehaviour
{
    [SerializeField] private string[] _inputs;

    public Ability[] Abilities { get; private set; }
    private PlayerInput _playerInput;

    private bool _isCasting;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        Abilities = GetComponents<Ability>();
    }
    private void Update()
    {
        CastAbilities();
    }
    private void CastAbilities()
    {
        for(int i = 0; i < Abilities.Length; i++)
        {
            if (_playerInput.actions[_inputs[i]].WasPressedThisFrame() && Abilities[i].CanCast(_isCasting))
            {
                Abilities[i].CastAbility(Mouse.Position);
            } 
            else if (_playerInput.actions[_inputs[i]].WasReleasedThisFrame())
            {
                Debug.Log($"Releasing ability {_inputs[i]}");
            }
        }
    }
}

