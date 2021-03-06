using Godot;

public class Wizard : KinematicBody2D
{
    private Vector2 _velocity = new Vector2();
    private const int MOVE_SPEED = 250;
    private AnimationPlayer _animationPlayer;
    private AnimationTree _animationTree;
    private AnimationNodeStateMachinePlayback _stateMachine;
    private Sprite _playerSprite;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _playerSprite = GetNode<Sprite>("Sprite");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _animationTree = GetNode<AnimationTree>("AnimationTree");
        _stateMachine = (AnimationNodeStateMachinePlayback)_animationTree.Get("parameters/playback");
        _animationTree.Active = true;
        _stateMachine.Start("idle");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if (Input.IsActionJustPressed("attack"))
        {
            _animationPlayer.Play("attack");
        }
        else
        {
            if (_animationPlayer.IsPlaying())
                return;
            if (_velocity.Length() > 0)
            {
                _stateMachine.Travel("run");
                _playerSprite.FlipH = _velocity.x < 0;
            }
            else
            {
                _stateMachine.Travel("idle");
            }
        }

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
