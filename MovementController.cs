using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    left,
    right
}

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 directionMovement = new Vector2(1f, 0f);
    [SerializeField] private Rigidbody2D rb;

    public Vector2 DirectionMovement { get => directionMovement; set => directionMovement = value; }
    public float Speed { get => speed; set => speed = value; }
    public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }

    private void Start()
    {
        Speed = BaseSpeed;
        rb = GetComponent<Rigidbody2D>();
        Moving();
    }

    public void StartMoving()
    {
        Speed = BaseSpeed;
        Moving();
    }

    public void StopMoving()
    {
        Speed = 0;
        Moving();
    }

    public void Moving()
    {
        rb.velocity = DirectionMovement * Speed;
    }
}
