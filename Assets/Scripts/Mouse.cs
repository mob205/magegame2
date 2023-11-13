using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public static Transform Position
    {
        get; private set;
    }
    private Camera _camera;

    private void Awake()
    {
        Position = transform;
        _camera = Camera.main;
    }
    private void Update()
    {
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
