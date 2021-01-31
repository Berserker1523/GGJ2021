using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GhostMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D ghostRigidbody;

    private void Awake()
    {
        ghostRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movementDirection)
    {
        ghostRigidbody.velocity = movementDirection;
    }
}

