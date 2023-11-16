using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse : MonoBehaviour
{
    public static Transform Position
    {
        get; private set;
    }
    private Camera _camera;
    private PlayerInput _input;

    private void Awake()
    {
        Position = transform;
        _input = PlayerInstance.Instance.GetComponent<PlayerInput>();
        _camera = Camera.main;
    }
    private void Update()
    {
        transform.position = _camera.ScreenToWorldPoint(_input.actions["Mouse Position"].ReadValue<Vector2>());
    }
}
