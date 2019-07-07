using Godot;
using System;

public class Lizard : KinematicBody2D
{
    private AnimatedSprite _animatedSprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _animatedSprite.Play("idle");
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
