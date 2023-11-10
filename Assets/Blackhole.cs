using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public float Force = 100;
    public float MaxSpeed = 20;
    private PlayerController _player;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }
    private void FixedUpdate()
    {
        float distance = (transform.position - _player.transform.position).magnitude;
        Vector2 direction = (transform.position - _player.transform.position).normalized;
        float magnitude = Force / Mathf.Sqrt(distance);

        _player.AddExternalAcceleration(direction, magnitude, MaxSpeed);
        //_player.AddExternalVelocity(Mathf.Clamp(magnitude, 0, MaxSpeed) * direction);
    }
}
