using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

enum EnemyState
{
    IDLE = 0,
    NewDirection = 1,
    Move = 2
}

public class Lizard : KinematicBody2D
{
    private Random _random = new Random();
    private EnemyState _state = EnemyState.NewDirection;
    private const int MoveSpeed = 50;
    private Vector2 _direction = Vector2.Right;
    private AnimatedSprite _animatedSprite;
    private Timer _timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _animatedSprite.Play("idle");
    }

    public override void _Process(float delta)
    {
        switch (_state)
        {
            case EnemyState.IDLE:
                break;
            case EnemyState.Move:
                Move(delta);
                break;
            case EnemyState.NewDirection:
                _direction = GetRandomValue<Vector2>(new List<Vector2> { Vector2.Down, Vector2.Up, Vector2.Right, Vector2.Left });
                _state = GetRandomValue<EnemyState>(new List<EnemyState> { EnemyState.IDLE, EnemyState.Move });
                break;
        }
    }

    private void OnTimerTimeout()
    {
        _timer.WaitTime = GetRandomValue<float>(new List<float> { 0.5f, 1, 1.5f });
        _state = GetRandomValue<EnemyState>(new List<EnemyState> { EnemyState.IDLE, EnemyState.Move, EnemyState.NewDirection });
    }

    private T GetRandomValue<T>(IList<T> values)
    {
        var i = _random.Next(values.Count());
        return values[i];
    }

    private Vector2 GetNewDirection()
    {
        return Vector2.Down;
    }

    private void Move(float delta)
    {
        Position += _direction * MoveSpeed * delta;
    }
}
