using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private LayerMask player;
    private bool _inRange;

    protected PlayerInput _inputs;

    protected virtual void Awake()
    {
        _inputs = PlayerInstance.Instance.GetComponent<PlayerInput>();
    }
    private void OnEnable() => _inputs.actions["Interact"].started += CheckInteract;
    private void OnDisable()
    {
        if (_inputs)
        {
            _inputs.actions["Interact"].started -= CheckInteract;
        }
    }
    // Check if player has entered range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(InLayer(collision.gameObject.layer, player))
        {
            _inRange = true;
        }
    }
    // Check if player has exited range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(InLayer(collision.gameObject.layer, player))
        {
            _inRange = false;
        }
    }
    private bool InLayer(int layer, LayerMask mask)
    {
        return mask == (mask | (1 << layer));
    }
    private void CheckInteract(InputAction.CallbackContext context)
    {
        if (_inRange)
        {
            Interact();
        }
    }
    protected virtual void Interact()
    {
        // Interaction implementation here
    }
}
