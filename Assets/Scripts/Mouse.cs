using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    public static Transform Position { get; private set; }
    private Camera _camera;
    private PlayerInput _input;

    private void Awake()
    {
        Position = transform;
        _input = PlayerInstance.Instance.GetComponent<PlayerInput>();
        _camera = Camera.main;
    }
    private void Start()
    {
        _input.actions["Select"].canceled += CastRay;
    }
    private void Update()
    {
        transform.position = _camera.ScreenToWorldPoint(_input.actions["Mouse Position"].ReadValue<Vector2>());
    }
    private void CastRay(InputAction.CallbackContext context)
    {
        var rawPos = _input.actions["Mouse Position"].ReadValue<Vector2>();
        var hit = Physics2D.Raycast(rawPos, Vector3.forward, Mathf.Infinity);
        if (!hit) { return; }
        var selectable = hit.collider.GetComponent<Selectable>();
        if (selectable)
        {
            selectable.Select();
        }
    }
}
