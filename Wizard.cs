using Godot;
using System;

public class Wizard : KinematicBody2D
{
    private enum State
    {
        IDLE = 0,
        RUN = 1,
        Attack = 2
    }
    private Vector2 _velocity = new Vector2();
    private const int MOVE_SPEED = 200;
    private AnimatedSprite _animatedSprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {


        // if (shouldAttach)
        // {
        //     _animatedSprite.Play("attack");
        // }
        // else
        // {
        if (_velocity.Length() > 0)
        {
            _animatedSprite.Play("run");
            _animatedSprite.FlipH = _velocity.x < 0;
        }
        else
        {
            _animatedSprite.Play("idle");
        }
        // }

    }

    public override void _PhysicsProcess(float delta)
    {
        HandleInput();
        _velocity = _velocity * delta * MOVE_SPEED;
        MoveAndCollide(_velocity);
    }

    private void HandleInput()
    {
        _velocity = new Vector2();

        if (Input.IsActionPressed("up"))
        {
            _velocity = Vector2.Up;
        }

        if (Input.IsActionPressed("down"))
        {
            _velocity = Vector2.Down;
        }

        if (Input.IsActionPressed("left"))
        {
            _velocity = Vector2.Left;
        }

        if (Input.IsActionPressed("right"))
        {
            _velocity = Vector2.Right;
        }

    }
}
