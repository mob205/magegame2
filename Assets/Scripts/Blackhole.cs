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
        var distance = (transform.position - _player.transform.position).magnitude;
        var direction = (transform.position - _player.transform.position).normalized;
        var magnitude = Force / Mathf.Sqrt(distance);

        _player.AddExternalAcceleration(direction, magnitude, MaxSpeed);
    }
}
