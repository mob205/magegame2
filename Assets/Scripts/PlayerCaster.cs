using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCaster : MonoBehaviour
{
    [SerializeField] private string[] _axes;
    private void Update()
    {
        CastAbilities();
    }
    private void CastAbilities()
    {
        for(int i = 0; i < _axes.Length; i++)
        {
            if (Input.GetButtonDown(_axes[i]))
            {
                Debug.Log($"Firing ability {_axes[i]}");
            } 
            else if (Input.GetButtonUp(_axes[i]))
            {
                Debug.Log($"Releasing abilit {_axes[i]}");
            }
        }
    }
}

