using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public float Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit!");
    }
}
