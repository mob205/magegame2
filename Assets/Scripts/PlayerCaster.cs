using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCaster : MonoBehaviour
{
    [SerializeField] private string[] _inputs;

    private PlayerInput _playerInput;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        CastAbilities();
    }
    private void CastAbilities()
    {
        for(int i = 0; i < _inputs.Length; i++)
        {
            if (_playerInput.actions[_inputs[i]].WasPressedThisFrame())
            {
                Debug.Log($"Firing ability {_inputs[i]}");
            } 
            else if (_playerInput.actions[_inputs[i]].WasReleasedThisFrame())
            {
                Debug.Log($"Releasing ability {_inputs[i]}");
            }
        }
    }
}

